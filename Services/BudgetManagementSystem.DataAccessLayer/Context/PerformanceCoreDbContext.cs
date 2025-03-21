using BudgetManagementSystem.DataAccessLayer.Concrete;
using BudgetManagementSystem.Models.CompetencyMgt;
using BudgetManagementSystem.ViewModels.DTOs;
using PMS.Models.Auditing;
using PMS.Models.CoreModels;

namespace BudgetManagementSystem.DataAccessLayer.Context;

public class PerformanceCoreDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
                                        string,
                                        IdentityUserClaim<string>,
                                        IdentityUserRole<string>,
                                        IdentityUserLogin<string>,
                                        IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    private readonly IUserDbContext _userContext;
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<PerformanceCoreDbContext> _logger;

    public PerformanceCoreDbContext(DbContextOptions<PerformanceCoreDbContext> options, IUserDbContext userContext,
                                    IDateTimeService dateTimeService, ILogger<PerformanceCoreDbContext> logger) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        _userContext = userContext;
        _dateTimeService = dateTimeService;
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Iterate through all EF Entity types
        DbContextHelper.TemporalTableAutomaticBuilder(builder); //Add Query Filter for SoftDelete
        DbContextHelper.SoftDeleteAutomaticBuilder(builder); //Add Query Filter for SoftDelete

        DbContextHelper.UniqueKeyAutomaticBuilder(builder); // Unique key and composite Key automation
        //DbContextHelper.SeedAppUser(builder); // Unique key and composite Key automation

        builder.HasDefaultSchema("pms");
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

    public virtual DbSet<AuditLog> AuditLogs { get; set; }
    public virtual DbSet<SequenceNumber> SequenceNumbers { get; set; }
    public virtual DbSet<AuditableAttribute> AuditableAttributes { get; set; }
    public virtual DbSet<AuditableEntity> AuditableEntities { get; set; }




    #region Audit Logs
    private async Task SaveAuditLog()
    {
        string username = string.Empty;

        var dbEntityEntries = ChangeTracker.Entries().ToList()
            .Where(p => p.State == EntityState.Modified || p.State == EntityState.Added || p.State == EntityState.Deleted);

        foreach (var dbEntityEntry in dbEntityEntries)
        {
            try
            {
                username = dbEntityEntry.State == EntityState.Modified ? ((BaseAudit)dbEntityEntry.Entity).UpdatedBy : ((BaseAudit)dbEntityEntry.Entity).CreatedBy;
                var auditLogs = AuditLogHelper.GetChangesForAuditLog(dbEntityEntry, username);
                foreach (var auditlog in auditLogs)
                    if (auditlog != null)
                      await  AuditLogs.AddAsync(auditlog);
            }
            catch
            {
                continue;
            }
        }
    }

    private async Task UpdateAuditLogRecordId()
    {
        foreach (var entity in AuditLogHelper.addedEntities)
        {
            if (ChangeTracker.Entries().ToList().Contains(entity.Value))
            {
                string keyName = entity.Value.Entity
                    .GetType()
                    .GetProperties()
                    .Single(p => p.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false).Count() > 0).Name;

                string recid = entity.Value.Property(keyName).CurrentValue.ToString();

                var auditLog = this.AuditLogs.FirstOrDefault(log => log.AuditEventDateUTC == entity.Key);

                if (auditLog != null)
                {
                    auditLog.RecordId = recid;
                   await base.SaveChangesAsync();
                }
            }
        }
    }
    #endregion

}
