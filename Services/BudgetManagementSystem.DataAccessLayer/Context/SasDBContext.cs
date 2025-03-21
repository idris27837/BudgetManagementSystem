using BudgetManagementSystem.Models.ErpModel;
using BudgetManagementSystem.Models.Sas;

namespace BudgetManagementSystem.DataAccessLayer.Context
{
    public class SasDBContext : DbContext
    {
        public SasDBContext(DbContextOptions<SasDBContext> options) : base(options)
        {
        }

        public DbSet<AbsenceMode> AbsenceModes { get; set; }
        public DbSet<StaffLunchAttendance> StaffLunchAttendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


    }
}
