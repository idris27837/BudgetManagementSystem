using CompetencyApp.Models.CompetencyMgt;
using CompetencyApp.ViewModels.DTOs;

namespace CompetencyApp.DataAccessLayer.Context;

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
        base.OnModelCreating(builder);
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
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ApplyUtcDateTimeConversion();
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
            _logger.LogInformation("Record {state} for {type} to database", entry.State, entry.Entity.GetType().Name);
        }
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
    public DbSet<Competency> Competencies { get; set; }
    public DbSet<CompetencyCategory> CompetencyCategories { get; set; }
    public DbSet<CompetencyCategoryGrading> CompetencyCategoryGradings { get; set; }
    public DbSet<CompetencyRatingDefinition> CompetencyRatingDefinitions { get; set; }
    public DbSet<CompetencyReview> CompetencyReviews { get; set; }
    public DbSet<CompetencyReviewProfile> CompetencyReviewProfiles { get; set; }
    public DbSet<DevelopmentPlan> DevelopmentPlans { get; set; }
    public DbSet<JobRole> JobRoles { get; set; }
    public DbSet<JobGrade> JobGrades { get; set; }
    public DbSet<JobGradeGroup> JobGradeGroups { get; set; }
    public DbSet<AssignJobGradeGroup> AssignJobGradeGroups { get; set; }
    public DbSet<OfficeJobRole> OfficeJobRole { get; set; }
    public DbSet<JobRoleCompetency> JobRoleCompetencies { get; set; }
    public DbSet<BehavioralCompetency> BehavioralCompetencies { get; set; }
    public DbSet<JobRoleGrade> JobRoleGrades { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<ReviewPeriod> ReviewPeriods { get; set; }
    public DbSet<ReviewType> ReviewTypes { get; set; }
    public DbSet<TrainingType> TrainingTypes { get; set; }
    public DbSet<StaffJobRoles> StaffJobRoles { get; set; }
    #endregion

}
