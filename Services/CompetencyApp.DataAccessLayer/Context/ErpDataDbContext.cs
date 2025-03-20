using CompetencyApp.Models.ErpModel;

namespace CompetencyApp.DataAccessLayer.Context;

public class ErpDataDbContext : DbContext
{
    public ErpDataDbContext(DbContextOptions<ErpDataDbContext> options) : base(options)
    {
    }

    public DbSet<EmployeeDetails> EmployeeDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeDetails>(entity =>
        {

            entity.HasKey(e => e.EmployeeNumber)
               .IsClustered(false);

            entity.ToTable("ERP_EMPLOYEE_DETAILS");

            entity.Property(e => e.EmployeeNumber)
                .HasMaxLength(10)
                .HasColumnName("EMPLOYEE_NUMBER");

            entity.Property(e => e.AssignmentId).HasColumnName("ASSIGNMENT_ID");

            entity.Property(e => e.AssignmentNumber)
                .HasMaxLength(10)
                .HasColumnName("ASSIGNMENT_NUMBER");

            entity.Property(e => e.BankAccountNumber)
                .HasMaxLength(100)
                .HasColumnName("BANK_ACCOUNT_NUMBER");

            entity.Property(e => e.BankCode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("BANK_CODE");

            entity.Property(e => e.CreationDate).HasColumnName("CREATION_DATE");

            entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(200)
                .HasColumnName("DEPARTMENT_NAME");

            entity.Property(e => e.DivisionId).HasColumnName("DIVISION_ID");

            entity.Property(e => e.DivisionName)
                .HasMaxLength(200)
                .HasColumnName("DIVISION_NAME");

            entity.Property(e => e.EmailAddress)
                .HasMaxLength(250)
                .HasColumnName("EMAIL_ADDRESS");

            entity.Property(e => e.FirstName)
                .HasMaxLength(150)
                .HasColumnName("FIRST_NAME");

            entity.Property(e => e.FullName)
                .HasMaxLength(300)
                .HasColumnName("FULL_NAME");

            entity.Property(e => e.GeoPoliticalZone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GEO_POLITICAL_ZONE");

            entity.Property(e => e.Grade)
                .HasMaxLength(10)
                .HasColumnName("GRADE");

            entity.Property(e => e.GradeId).HasColumnName("GRADE_ID");

            entity.Property(e => e.HeadOfDeptId)
                .HasMaxLength(10)
                .HasColumnName("HEAD_OF_DEPT_ID");

            entity.Property(e => e.HeadOfDeptName)
                .HasMaxLength(250)
                .HasColumnName("HEAD_OF_DEPT_NAME");

            entity.Property(e => e.HeadOfDivId)
                .HasMaxLength(10)
                .HasColumnName("HEAD_OF_DIV_ID");

            entity.Property(e => e.HeadOfDivName)
                .HasMaxLength(300)
                .HasColumnName("HEAD_OF_DIV_NAME");

            entity.Property(e => e.HeadOfOfficeId)
                .HasMaxLength(10)
                .HasColumnName("HEAD_OF_OFFICE_ID");

            entity.Property(e => e.JobName)
                .HasMaxLength(100)
                .HasColumnName("JOB_NAME");

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("LAST_NAME");

            entity.Property(e => e.LastUpdateDate).HasColumnName("LAST_UPDATE_DATE");

            entity.Property(e => e.Lga)
                .HasMaxLength(10)
                .HasColumnName("LGA");

            entity.Property(e => e.LocationCode)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("LOCATION_CODE");

            entity.Property(e => e.LocationId).HasColumnName("LOCATION_ID");

            entity.Property(e => e.MiddleNames)
                .HasMaxLength(150)
                .HasColumnName("MIDDLE_NAMES");

            entity.Property(e => e.MobileNumber)
                .HasMaxLength(50)
                .HasColumnName("MOBILE_NUMBER");

            entity.Property(e => e.OfficeId).HasColumnName("OFFICE_ID");

            entity.Property(e => e.OfficeName)
                .HasMaxLength(200)
                .HasColumnName("OFFICE_NAME");

            entity.Property(e => e.OrganizationId).HasColumnName("ORGANIZATION_ID");

            entity.Property(e => e.OriginalDateOfHire).HasColumnName("ORIGINAL_DATE_OF_HIRE");

            entity.Property(e => e.PersonId).HasColumnName("PERSON_ID");

            entity.Property(e => e.PersonTypeId).HasColumnName("PERSON_TYPE_ID");

            entity.Property(e => e.Position)
                .HasMaxLength(400)
                .HasColumnName("POSITION");

            entity.Property(e => e.Religion)
                .HasMaxLength(200)
                .HasColumnName("RELIGION");

            entity.Property(e => e.SenatorialDistrict)
                .HasMaxLength(200)
                .HasColumnName("SENATORIAL_DISTRICT");

            entity.Property(e => e.StateOfOrigin)
                .HasMaxLength(10)
                .HasColumnName("STATE_OF_ORIGIN");

            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("STATUS");

            entity.Property(e => e.SupervisorId)
                .HasMaxLength(10)
                .HasColumnName("SUPERVISOR_ID");

            entity.Property(e => e.TerminationDate).HasColumnName("TERMINATION_DATE");

            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("TITLE");

            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("USER_NAME");

            entity.Property(e => e.WalletAlias)
                .HasMaxLength(100)
                .HasColumnName("WALLET_ALIAS");

            entity.Property(e => e.WalletId)
                .HasMaxLength(400)
                .HasColumnName("WALLET_ID");
        });


        // OnModelCreatingPartial(modelBuilder);
    }

    //  private void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
