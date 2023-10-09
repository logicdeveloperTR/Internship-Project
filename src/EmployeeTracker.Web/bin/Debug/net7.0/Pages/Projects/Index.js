$(function () {
    var createModal = new abp.ModalManager(abp.appPath + 'Projects/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Projects/EditModal');

    var projectService = employeeTracker.projects.project;

    var dataTable = $('#ProjectsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(projectService.getList),
            columnDefs: [
                {
                    title: 'Actions',
                    rowAction: {
                        items:
                            [
                                {
                                    text: 'Edit',
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: 'Delete',
                                    confirmMessage: function (data) {
                                        return "Are you sure to delete the book '" + data.record.name  +"'?";
                                    },
                                    action: function (data) {
                                        projectService
                                            .delete(data.record.id)
                                            .then(function() {
                                                abp.notify.info("Successfully deleted!");
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: 'Name',
                    data: "name"
                },
                {
                    title: 'Description',
                    data: "description"
                },
                {
                    title: 'Start Time',
                    data: "startTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    }
                },
                {
                    title: 'End Time',
                    data: "endTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    }
                },
                
                
                {
                    title: 'Leader Names',
                    data: "leaderNames",
                    render: function (data) {
                        return data.join(", ");
                    }
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewProjectButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});