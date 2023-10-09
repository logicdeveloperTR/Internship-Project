$(function () {
    var createModal = new abp.ModalManager(abp.appPath + 'Employees/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Employees/EditModal');
    var l = abp.localization.getResource('EmployeeTracker');

    var employeeService = employeeTracker.employees.employee;

    var dataTable = $('#EmployeesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(employeeService.getList),
            columnDefs: [
                {
                    title: 'Actions',
                    rowAction:
                    {
                        items:
                            [
                                {
                                    text: 'Edit',
                                    visible: abp.auth.isGranted('EmployeeTracker.Employees.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: 'Delete',
                                    visible: abp.auth.isGranted('EmployeeTracker.Employees.Delete'),
                                    confirmMessage: function (data) {
                                        return "Are you sure to delete the book '" + data.record.name  +"'?";
                                    },
                                    action: function (data) {
                                        employeeService
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
                    title: 'Department',
                    data: "department",
                    render: function (data) {
                        return l('Enum:EmployeeDepartment.' + data);
                    }
                },

                {
                    title: 'Salary',
                    data: "salary"
                },
                {
                    title: 'Manager Name',
                    data: "headName"
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

    $('#NewEmployeeButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});