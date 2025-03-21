using BudgetManagementSystem.Models.ErpModel;

namespace BudgetManagementSystem.DataAccessLayer.Context
{
    public class EmailServiceDBContext : DbContext
    {
        public EmailServiceDBContext(DbContextOptions<EmailServiceDBContext> options) : base(options)
        {
        }

        public DbSet<EmailObjects> EmailLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailObjects>(entity =>
            {
                entity.ToTable("EmailLogs");

                entity.HasKey(e => e.Id).IsClustered(false);

                // Configure Id to be an identity column
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // This marks it as an auto-increment column

            });

        }


    }
}
