using EmployeeTracker.Employees;
using EmployeeTracker.HeadLists;
using EmployeeTracker.Heads;
using EmployeeTracker.LeaderLists;
using EmployeeTracker.Leaders;
using EmployeeTracker.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace EmployeeTracker.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class EmployeeTrackerDbContext :
    AbpDbContext<EmployeeTrackerDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    public DbSet<Head> Heads { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Leader> Leaders { get; set; }

    public DbSet<LeaderList> LeadersList { get; set; }

    public DbSet<HeadList> HeadList { get; set; }

    public DbSet<Project> Projects { get; set; }
    public EmployeeTrackerDbContext(DbContextOptions<EmployeeTrackerDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(EmployeeTrackerConsts.DbTablePrefix + "YourEntities", EmployeeTrackerConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Employee>(b =>
        {
           
            b.ToTable(EmployeeTrackerConsts.DbTablePrefix + "Employees", EmployeeTrackerConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Description);
            b.Property(x => x.Name).HasMaxLength(ProjectConsts.MaxStr).IsRequired();
            b.Property(x => x.StartTime)
            .HasDefaultValue(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            
            b.HasMany(x => x.HeadNames).WithOne().HasForeignKey(x => x.EmployeeId).IsRequired();

        });

        builder.Entity<Head>(b =>
        {
            b.ToTable(EmployeeTrackerConsts.DbTablePrefix + "EmployeeHeads", EmployeeTrackerConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name).HasMaxLength(ProjectConsts.MaxStr).IsRequired();
        });

        builder.Entity<HeadList>(b =>
        {
            b.ToTable(EmployeeTrackerConsts.DbTablePrefix + "EmployeeHeadList", EmployeeTrackerConsts.DbSchema);
            b.ConfigureByConvention();

            //define composite key
            b.HasKey(x => new { x.EmployeeId, x.HeadId });
            
            //many-to-many configuration
            b.HasOne<Employee>().WithMany(x => x.HeadNames).HasForeignKey(x => x.EmployeeId).IsRequired();
            b.HasOne<Head>().WithMany().HasForeignKey(x => x.HeadId).IsRequired();

            b.HasIndex(x => new { x.EmployeeId, x.HeadId });
        });

        builder.Entity<Project>(b =>
        {
            b.ToTable(EmployeeTrackerConsts.DbTablePrefix + "Projects", EmployeeTrackerConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Description);
            b.Property(x => x.Name).HasMaxLength(ProjectConsts.MaxStr).IsRequired();
            b.Property(x => x.StartTime)
            .HasDefaultValue(new DateTime(DateTime.Now.Year, DateTime.Now.Month-1, DateTime.Now.Day));
            b.Property(x => x.EndTime)
            .HasDefaultValue(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));


            b.HasMany(x => x.LeaderNames).WithOne().HasForeignKey(x => x.ProjectId).IsRequired();

        });

        builder.Entity<Leader>(b =>
        {
            b.ToTable(EmployeeTrackerConsts.DbTablePrefix + "ProjectLeaders", EmployeeTrackerConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name).HasMaxLength(ProjectConsts.MaxStr).IsRequired();
        });

        builder.Entity<LeaderList>(b =>
        {
            b.ToTable(EmployeeTrackerConsts.DbTablePrefix + "LeaderList", EmployeeTrackerConsts.DbSchema);
            b.ConfigureByConvention();

            //define composite key
            b.HasKey(x => new { x.ProjectId, x.LeaderId });

            //many-to-many configuration
            b.HasOne<Project>().WithMany(x => x.LeaderNames).HasForeignKey(x => x.ProjectId).IsRequired();
            b.HasOne<Leader>().WithMany().HasForeignKey(x => x.LeaderId).IsRequired();

            b.HasIndex(x => new { x.ProjectId, x.LeaderId });
        });
    }
}
