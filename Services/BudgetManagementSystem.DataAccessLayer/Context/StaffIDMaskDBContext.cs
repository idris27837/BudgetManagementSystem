using BudgetManagementSystem.Models.ErpModel;

namespace BudgetManagementSystem.DataAccessLayer.Context
{
    public class StaffIDMaskDBContext : DbContext
    {
        public StaffIDMaskDBContext(DbContextOptions<StaffIDMaskDBContext> options) : base(options)
        {
        }

        public DbSet<StaffIDMaskDetails> StaffIDMaskDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StaffIDMaskDetails>(entity =>
            {

                entity.HasKey(e => e.EmployeeNumber).IsClustered(false);

                entity.ToTable("StaffIdMask");

                entity.Property(e => e.EmployeeNumber).HasMaxLength(10).HasColumnName("EmployeeNumber");

                entity.Property(e => e.Name).HasColumnName("Name");

                entity.Property(e => e.CurrentPicture).HasColumnName("CurrentPicture");

                entity.Property(e => e.NewPicture).HasColumnName("NewPicture");

                entity.Property(e => e.BloodGroup).HasColumnName("BloodGroup");

                entity.Property(e => e.ApprovedBy).HasColumnName("ApprovedBy");

                entity.Property(e => e.ApprovalDate).HasColumnName("ApprovalDate");

                entity.Property(e => e.RejectReason).HasColumnName("RejectReason");

                entity.Property(e => e.Rejectedby).HasColumnName("Rejectedby");

                entity.Property(e => e.RejectionDate).HasColumnName("RejectionDate");

                entity.Property(e => e.Status).HasColumnName("Status");

                entity.Property(e => e.CreateDate).HasColumnName("CreateDate");

                entity.Property(e => e.CreatedBy).HasColumnName("CreatedBy");

                entity.Property(e => e.LastUpdatedBy).HasColumnName("LastUpdatedBy");

                entity.Property(e => e.LastUpdateDate).HasColumnName("LastUpdateDate");

                entity.Property(e => e.MessageStatus).HasColumnName("MessageStatus");

            });

        }


    }
}
