using BudgetManagementSystem.DataAccessLayer.Concrete;
using BudgetManagementSystem.Models.BudgetMgt;
using BudgetManagementSystem.Models.ErpModel;
using BudgetManagementSystem.ViewModels.DTOs;
using Innofactor.EfCoreJsonValueConverter;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PMS.Models.Auditing;
using PMS.Models.CoreModels;
using PMS.Models.PerformanceMgt.CoreModels;
using static Dapper.SqlMapper;

namespace BudgetManagementSystem.DataAccessLayer.Context;

public class CompetencyCoreDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
                                        string,
                                        IdentityUserClaim<string>,
                                        IdentityUserRole<string>,
                                        IdentityUserLogin<string>,
                                        IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    private readonly IUserDbContext _userContext;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<CompetencyCoreDbContext> _logger;

    public CompetencyCoreDbContext(DbContextOptions<CompetencyCoreDbContext> options, IUserDbContext userContext,
                                    IDateTimeService dateTimeService, ILogger<CompetencyCoreDbContext> logger) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        _userContext = userContext;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        // Configure the many-to-many relationship between courses and prerequisites
        //builder.Entity<CompetencyReview>()
        //     .HasKey(c => new { c.RatingId, c.ExpectedRatingId });

        //builder.Entity<CompetencyReview>().HasOne(c => c.Rating)
        //    .WithMany(c => c.CompetencyReviews)
        //    .HasForeignKey(c => c.CompetencyId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //builder.Entity<CompetencyReview>().HasOne(c => c.ExpectedRating).WithMany()
        //    .HasForeignKey(c => c.ExpectedRatingId)
        //    .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore<WorkProductDefinition>();
        builder.AddJsonFields();
        builder.Entity<Competency>()
            .HasMany(c => c.CompetencyReviews)
            .WithOne(cr => cr.Competency)
            .HasForeignKey(cr => cr.CompetencyId); // Use CompetencyId as the foreign key for this relationship

        builder.Entity<StaffJobRoles>()
             .HasKey(c => new { c.StaffJobRoleId });

        // Iterate through all EF Entity types
        DbContextHelper.TemporalTableAutomaticBuilder(builder); //Add Query Filter for SoftDelete
        DbContextHelper.SoftDeleteAutomaticBuilder(builder); //Add Query Filter for SoftDelete

        DbContextHelper.UniqueKeyAutomaticBuilder(builder); // Unique key and composite Key automation
        //DbContextHelper.SeedAppUser(builder); // Unique key and composite Key automation

        builder.HasDefaultSchema("CoreSchema");
        base.OnModelCreating(builder);

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
       // ApplyUtcDateTimeConversion();
        foreach (var entry in ChangeTracker.Entries<BaseAudit>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Status = "CREATE";
                    entry.Entity.DateCreated = _dateTimeService.NowUtc;
                    entry.Entity.CreatedBy = _userContext?.UserId ?? "SYSTEM";
                    break;
                case EntityState.Modified:
                    entry.Entity.Status = entry.Entity.SoftDeleted ? "DELETED" : "UPDATE";
                    entry.Entity.DateUpdated = _dateTimeService.NowUtc;
                    entry.Entity.UpdatedBy = _userContext?.UserId ?? "SYSTEM";
                    break;
            }
            //_logger.LogInformation("Record {state} for {type} to database", entry.State, entry.Entity.GetType().Name);
        }

        foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    // entry.Entity.Status = "CREATE";
                    entry.Entity.CreatedAt = _dateTimeService.NowUtc;
                    entry.Entity.CreatedBy = _userContext.UserId.IsNullOrEmpty() ? "SYSTEM" : _userContext?.UserId;

                    break;
                case EntityState.Modified:
                    /// entry.Entity.Status = entry.Entity.SoftDeleted ? "DELETED" : "UPDATE";
                    entry.Entity.UpdatedAt = _dateTimeService.NowUtc;
                    entry.Entity.UpdatedBy = _userContext.UserId.IsNullOrEmpty() ? "SYSTEM" : _userContext?.UserId;
                    break;
            }
            //_logger.LogInformation("Record {state} for {type} to database", entry.State, entry.Entity.GetType().Name);
        }

        await SaveAuditLog();
        await UpdateAuditLogRecordId();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyUtcDateTimeConversion()
    {
        var utcNow = DateTime.UtcNow;
        var entities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entityEntry in entities)
        {
            foreach (var propertyEntry in entityEntry.Properties)
            {
                if (propertyEntry.CurrentValue is DateTime dateTime)
                {
                    if (propertyEntry.Metadata.IsNullable && dateTime.Kind == DateTimeKind.Unspecified)
                    {
                        propertyEntry.CurrentValue = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                    }
                    else if (dateTime.Kind != DateTimeKind.Utc)
                    {
                        propertyEntry.CurrentValue = dateTime.ToUniversalTime();
                    }
                }
            }
        }
    }

    #region Core Model
    public DbSet<BankYear> BankYears { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }

    #endregion


    #region Organogram Model
    public DbSet<Department> Department { get; set; }
    public DbSet<Directorate> Directorates { get; set; }
    public DbSet<Division> Divisions { get; set; }
    public DbSet<Office> Offices { get; set; }
    #endregion


    #region Organogram Model
    public DbSet<ReviewPeriod> ReviewPeriods { get; set; }
    public DbSet<ReviewType> ReviewTypes { get; set; }
    #endregion


}
