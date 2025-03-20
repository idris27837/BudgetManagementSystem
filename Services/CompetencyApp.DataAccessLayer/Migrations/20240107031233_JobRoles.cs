using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CompetencyApp.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class JobRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CoreSchema");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "CoreSchema",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "CoreSchema",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankYears",
                schema: "CoreSchema",
                columns: table => new
                {
                    BankYearId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    YearName = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankYears", x => x.BankYearId);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyCategories",
                schema: "CoreSchema",
                columns: table => new
                {
                    CompetencyCategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsTechnical = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyCategories", x => x.CompetencyCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyReviewProfiles",
                schema: "CoreSchema",
                columns: table => new
                {
                    CompetencyReviewProfileId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReviewPeriodId = table.Column<int>(type: "integer", nullable: false),
                    ReviewPeriodName = table.Column<string>(type: "text", nullable: true),
                    AverageRatingId = table.Column<int>(type: "integer", nullable: false),
                    AverageRatingName = table.Column<string>(type: "text", nullable: true),
                    AverageRatingValue = table.Column<int>(type: "integer", nullable: false),
                    ExpectedRatingId = table.Column<int>(type: "integer", nullable: false),
                    ExpectedRatingName = table.Column<string>(type: "text", nullable: true),
                    ExpectedRatingValue = table.Column<int>(type: "integer", nullable: false),
                    AverageScore = table.Column<double>(type: "double precision", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "text", nullable: true),
                    EmployeeName = table.Column<string>(type: "text", nullable: true),
                    CompetencyId = table.Column<int>(type: "integer", nullable: false),
                    CompetencyName = table.Column<string>(type: "text", nullable: true),
                    CompetencyCategoryName = table.Column<string>(type: "text", nullable: true),
                    CompetencyGap = table.Column<int>(type: "integer", nullable: false),
                    HaveGap = table.Column<bool>(type: "boolean", nullable: false),
                    OfficeId = table.Column<string>(type: "text", nullable: true),
                    OfficeName = table.Column<string>(type: "text", nullable: true),
                    DivisionId = table.Column<string>(type: "text", nullable: true),
                    DivisionName = table.Column<string>(type: "text", nullable: true),
                    DepartmentId = table.Column<string>(type: "text", nullable: true),
                    DepartmentName = table.Column<string>(type: "text", nullable: true),
                    JobRoleId = table.Column<string>(type: "text", nullable: true),
                    JobRoleName = table.Column<string>(type: "text", nullable: true),
                    GradeName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyReviewProfiles", x => x.CompetencyReviewProfileId);
                });

            migrationBuilder.CreateTable(
                name: "Directorates",
                schema: "CoreSchema",
                columns: table => new
                {
                    DirectorateId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DirectorateName = table.Column<string>(type: "text", nullable: true),
                    DirectorateCode = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directorates", x => x.DirectorateId);
                });

            migrationBuilder.CreateTable(
                name: "JobGradeGroups",
                schema: "CoreSchema",
                columns: table => new
                {
                    JobGradeGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobGradeGroups", x => x.JobGradeGroupId);
                });

            migrationBuilder.CreateTable(
                name: "JobGrades",
                schema: "CoreSchema",
                columns: table => new
                {
                    JobGradeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GradeCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    GradeName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobGrades", x => x.JobGradeId);
                });

            migrationBuilder.CreateTable(
                name: "JobRoles",
                schema: "CoreSchema",
                columns: table => new
                {
                    JobRoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobRoleName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRoles", x => x.JobRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "CoreSchema",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                schema: "CoreSchema",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                });

            migrationBuilder.CreateTable(
                name: "ReviewTypes",
                schema: "CoreSchema",
                columns: table => new
                {
                    ReviewTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReviewTypeName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewTypes", x => x.ReviewTypeId);
                });

            migrationBuilder.CreateTable(
            name: "StaffJobRoles",
                schema: "CoreSchema",
                columns: table => new
                {
                    StaffJobRoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<string>(type: "text", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    DivisionId = table.Column<int>(type: "integer", nullable: false),
                    OfficeId = table.Column<int>(type: "integer", nullable: false),
                    JobRoleId = table.Column<int>(type: "integer", nullable: false),
                    JobRoleName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    DateApproved = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffJobRoles", x => x.StaffJobRoleId);
                });

            migrationBuilder.CreateTable(
                name: "TrainingTypes",
                schema: "CoreSchema",
                columns: table => new
                {
                    TrainingTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrainingTypeName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingTypes", x => x.TrainingTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "CoreSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "CoreSchema",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "CoreSchema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "CoreSchema",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "CoreSchema",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "CoreSchema",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "CoreSchema",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "CoreSchema",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "CoreSchema",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "CoreSchema",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "CoreSchema",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewPeriods",
                schema: "CoreSchema",
                columns: table => new
                {
                    ReviewPeriodId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BankYearId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    DateApproved = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewPeriods", x => x.ReviewPeriodId);
                    table.ForeignKey(
                        name: "FK_ReviewPeriods_BankYears_BankYearId",
                        column: x => x.BankYearId,
                        principalSchema: "CoreSchema",
                        principalTable: "BankYears",
                        principalColumn: "BankYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competencies",
                schema: "CoreSchema",
                columns: table => new
                {
                    CompetencyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetencyCategoryId = table.Column<int>(type: "integer", nullable: false),
                    CompetencyName = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true),
                    DateApproved = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.CompetencyId);
                    table.ForeignKey(
                        name: "FK_Competencies_CompetencyCategories_CompetencyCategoryId",
                        column: x => x.CompetencyCategoryId,
                        principalSchema: "CoreSchema",
                        principalTable: "CompetencyCategories",
                        principalColumn: "CompetencyCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "CoreSchema",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DirectorateId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentName = table.Column<string>(type: "text", nullable: true),
                    DepartmentCode = table.Column<string>(type: "text", nullable: true),
                    IsBranch = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Department_Directorates_DirectorateId",
                        column: x => x.DirectorateId,
                        principalSchema: "CoreSchema",
                        principalTable: "Directorates",
                        principalColumn: "DirectorateId");
                });

            migrationBuilder.CreateTable(
                name: "AssignJobGradeGroups",
                schema: "CoreSchema",
                columns: table => new
                {
                    AssignJobGradeGroupId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobGradeGroupId = table.Column<int>(type: "integer", nullable: false),
                    JobGradeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignJobGradeGroups", x => x.AssignJobGradeGroupId);
                    table.ForeignKey(
                        name: "FK_AssignJobGradeGroups_JobGradeGroups_JobGradeGroupId",
                        column: x => x.JobGradeGroupId,
                        principalSchema: "CoreSchema",
                        principalTable: "JobGradeGroups",
                        principalColumn: "JobGradeGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignJobGradeGroups_JobGrades_JobGradeId",
                        column: x => x.JobGradeId,
                        principalSchema: "CoreSchema",
                        principalTable: "JobGrades",
                        principalColumn: "JobGradeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobRoleGrades",
                schema: "CoreSchema",
                columns: table => new
                {
                    JobRoleGradeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobRoleId = table.Column<int>(type: "integer", nullable: false),
                    GradeId = table.Column<string>(type: "text", nullable: true),
                    GradeName = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRoleGrades", x => x.JobRoleGradeId);
                    table.ForeignKey(
                        name: "FK_JobRoleGrades_JobRoles_JobRoleId",
                        column: x => x.JobRoleId,
                        principalSchema: "CoreSchema",
                        principalTable: "JobRoles",
                        principalColumn: "JobRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "CoreSchema",
                columns: table => new
                {
                    RolePermissionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RolePermissionId);
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "CoreSchema",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "CoreSchema",
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyCategoryGradings",
                schema: "CoreSchema",
                columns: table => new
                {
                    CompetencyCategoryGradingId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetencyCategoryId = table.Column<int>(type: "integer", nullable: false),
                    ReviewTypeId = table.Column<int>(type: "integer", nullable: false),
                    WeightPercentage = table.Column<double>(type: "double precision", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyCategoryGradings", x => x.CompetencyCategoryGradingId);
                    table.ForeignKey(
                        name: "FK_CompetencyCategoryGradings_CompetencyCategories_CompetencyC~",
                        column: x => x.CompetencyCategoryId,
                        principalSchema: "CoreSchema",
                        principalTable: "CompetencyCategories",
                        principalColumn: "CompetencyCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyCategoryGradings_ReviewTypes_ReviewTypeId",
                        column: x => x.ReviewTypeId,
                        principalSchema: "CoreSchema",
                        principalTable: "ReviewTypes",
                        principalColumn: "ReviewTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BehavioralCompetencies",
                schema: "CoreSchema",
                columns: table => new
                {
                    BehavioralCompetencyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetencyId = table.Column<int>(type: "integer", nullable: false),
                    JobGradeGroupId = table.Column<int>(type: "integer", nullable: false),
                    RatingId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehavioralCompetencies", x => x.BehavioralCompetencyId);
                    table.ForeignKey(
                        name: "FK_BehavioralCompetencies_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalSchema: "CoreSchema",
                        principalTable: "Competencies",
                        principalColumn: "CompetencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BehavioralCompetencies_JobGradeGroups_JobGradeGroupId",
                        column: x => x.JobGradeGroupId,
                        principalSchema: "CoreSchema",
                        principalTable: "JobGradeGroups",
                        principalColumn: "JobGradeGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BehavioralCompetencies_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalSchema: "CoreSchema",
                        principalTable: "Ratings",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyRatingDefinitions",
                schema: "CoreSchema",
                columns: table => new
                {
                    CompetencyRatingDefinitionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetencyId = table.Column<int>(type: "integer", nullable: false),
                    RatingId = table.Column<int>(type: "integer", nullable: false),
                    Definition = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyRatingDefinitions", x => x.CompetencyRatingDefinitionId);
                    table.ForeignKey(
                        name: "FK_CompetencyRatingDefinitions_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalSchema: "CoreSchema",
                        principalTable: "Competencies",
                        principalColumn: "CompetencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyRatingDefinitions_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalSchema: "CoreSchema",
                        principalTable: "Ratings",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetencyReviews",
                schema: "CoreSchema",
                columns: table => new
                {
                    CompetencyReviewId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeNumber = table.Column<string>(type: "text", nullable: true),
                    ReviewPeriodId = table.Column<int>(type: "integer", nullable: false),
                    CompetencyId = table.Column<int>(type: "integer", nullable: false),
                    ReviewTypeId = table.Column<int>(type: "integer", nullable: false),
                    ExpectedRatingId = table.Column<int>(type: "integer", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReviewerId = table.Column<string>(type: "text", nullable: true),
                    ReviewerName = table.Column<string>(type: "text", nullable: true),
                    ActualRatingId = table.Column<int>(type: "integer", nullable: false),
                    ActualRatingName = table.Column<string>(type: "text", nullable: true),
                    ActualRatingValue = table.Column<int>(type: "integer", nullable: false),
                    IsTechnical = table.Column<bool>(type: "boolean", nullable: false),
                    EmployeeName = table.Column<string>(type: "text", nullable: true),
                    EmployeeInitial = table.Column<string>(type: "text", nullable: true),
                    EmployeeGrade = table.Column<string>(type: "text", nullable: true),
                    EmployeeDepartment = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetencyReviews", x => x.CompetencyReviewId);
                    table.ForeignKey(
                        name: "FK_CompetencyReviews_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalSchema: "CoreSchema",
                        principalTable: "Competencies",
                        principalColumn: "CompetencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyReviews_Ratings_ExpectedRatingId",
                        column: x => x.ExpectedRatingId,
                        principalSchema: "CoreSchema",
                        principalTable: "Ratings",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyReviews_ReviewPeriods_ReviewPeriodId",
                        column: x => x.ReviewPeriodId,
                        principalSchema: "CoreSchema",
                        principalTable: "ReviewPeriods",
                        principalColumn: "ReviewPeriodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetencyReviews_ReviewTypes_ReviewTypeId",
                        column: x => x.ReviewTypeId,
                        principalSchema: "CoreSchema",
                        principalTable: "ReviewTypes",
                        principalColumn: "ReviewTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                schema: "CoreSchema",
                columns: table => new
                {
                    DivisionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    DivisionName = table.Column<string>(type: "text", nullable: true),
                    DivisionCode = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.DivisionId);
                    table.ForeignKey(
                        name: "FK_Divisions_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "CoreSchema",
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevelopmentPlans",
                schema: "CoreSchema",
                columns: table => new
                {
                    DevelopmentPlanId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompetencyReviewProfileId = table.Column<int>(type: "integer", nullable: false),
                    TrainingTypeName = table.Column<string>(type: "text", nullable: true),
                    Activity = table.Column<string>(type: "text", nullable: true),
                    EmployeeNumber = table.Column<string>(type: "text", nullable: true),
                    TargetDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TaskStatus = table.Column<string>(type: "text", nullable: true),
                    LearningResource = table.Column<string>(type: "text", nullable: true),
                    CompetencyReviewId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevelopmentPlans", x => x.DevelopmentPlanId);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlans_CompetencyReviewProfiles_CompetencyReviewP~",
                        column: x => x.CompetencyReviewProfileId,
                        principalSchema: "CoreSchema",
                        principalTable: "CompetencyReviewProfiles",
                        principalColumn: "CompetencyReviewProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevelopmentPlans_CompetencyReviews_CompetencyReviewId",
                        column: x => x.CompetencyReviewId,
                        principalSchema: "CoreSchema",
                        principalTable: "CompetencyReviews",
                        principalColumn: "CompetencyReviewId");
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                schema: "CoreSchema",
                columns: table => new
                {
                    OfficeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DivisionId = table.Column<int>(type: "integer", nullable: false),
                    OfficeName = table.Column<string>(type: "text", nullable: true),
                    OfficeCode = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.OfficeId);
                    table.ForeignKey(
                        name: "FK_Offices_Divisions_DivisionId",
                        column: x => x.DivisionId,
                        principalSchema: "CoreSchema",
                        principalTable: "Divisions",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobRoleCompetencies",
                schema: "CoreSchema",
                columns: table => new
                {
                    JobRoleCompetencyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OfficeId = table.Column<int>(type: "integer", nullable: false),
                    JobRoleId = table.Column<int>(type: "integer", nullable: false),
                    CompetencyId = table.Column<int>(type: "integer", nullable: false),
                    RatingId = table.Column<int>(type: "integer", nullable: false),
                    ReviewPeriodId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRoleCompetencies", x => x.JobRoleCompetencyId);
                    table.ForeignKey(
                        name: "FK_JobRoleCompetencies_Competencies_CompetencyId",
                        column: x => x.CompetencyId,
                        principalSchema: "CoreSchema",
                        principalTable: "Competencies",
                        principalColumn: "CompetencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobRoleCompetencies_JobRoles_JobRoleId",
                        column: x => x.JobRoleId,
                        principalSchema: "CoreSchema",
                        principalTable: "JobRoles",
                        principalColumn: "JobRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobRoleCompetencies_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalSchema: "CoreSchema",
                        principalTable: "Offices",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobRoleCompetencies_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalSchema: "CoreSchema",
                        principalTable: "Ratings",
                        principalColumn: "RatingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobRoleCompetencies_ReviewPeriods_ReviewPeriodId",
                        column: x => x.ReviewPeriodId,
                        principalSchema: "CoreSchema",
                        principalTable: "ReviewPeriods",
                        principalColumn: "ReviewPeriodId");
                });

            migrationBuilder.CreateTable(
                name: "OfficeJobRole",
                schema: "CoreSchema",
                columns: table => new
                {
                    OfficeJobRoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OfficeId = table.Column<int>(type: "integer", nullable: false),
                    JobRoleId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    SoftDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeJobRole", x => x.OfficeJobRoleId);
                    table.ForeignKey(
                        name: "FK_OfficeJobRole_JobRoles_JobRoleId",
                        column: x => x.JobRoleId,
                        principalSchema: "CoreSchema",
                        principalTable: "JobRoles",
                        principalColumn: "JobRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficeJobRole_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalSchema: "CoreSchema",
                        principalTable: "Offices",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "CoreSchema",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "CoreSchema",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "CoreSchema",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "CoreSchema",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "CoreSchema",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "CoreSchema",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "CoreSchema",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignJobGradeGroups_JobGradeGroupId",
                schema: "CoreSchema",
                table: "AssignJobGradeGroups",
                column: "JobGradeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignJobGradeGroups_JobGradeId",
                schema: "CoreSchema",
                table: "AssignJobGradeGroups",
                column: "JobGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignJobGradeGroups_SoftDeleted",
                schema: "CoreSchema",
                table: "AssignJobGradeGroups",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BankYears_SoftDeleted",
                schema: "CoreSchema",
                table: "BankYears",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_BankYears_YearName",
                schema: "CoreSchema",
                table: "BankYears",
                column: "YearName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BehavioralCompetencies_CompetencyId_JobGradeGroupId",
                schema: "CoreSchema",
                table: "BehavioralCompetencies",
                columns: new[] { "CompetencyId", "JobGradeGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BehavioralCompetencies_JobGradeGroupId",
                schema: "CoreSchema",
                table: "BehavioralCompetencies",
                column: "JobGradeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BehavioralCompetencies_RatingId",
                schema: "CoreSchema",
                table: "BehavioralCompetencies",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_BehavioralCompetencies_SoftDeleted",
                schema: "CoreSchema",
                table: "BehavioralCompetencies",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_CompetencyCategoryId",
                schema: "CoreSchema",
                table: "Competencies",
                column: "CompetencyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_CompetencyName",
                schema: "CoreSchema",
                table: "Competencies",
                column: "CompetencyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Competencies_SoftDeleted",
                schema: "CoreSchema",
                table: "Competencies",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyCategories_CategoryName",
                schema: "CoreSchema",
                table: "CompetencyCategories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyCategories_SoftDeleted",
                schema: "CoreSchema",
                table: "CompetencyCategories",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyCategoryGradings_CompetencyCategoryId",
                schema: "CoreSchema",
                table: "CompetencyCategoryGradings",
                column: "CompetencyCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyCategoryGradings_ReviewTypeId",
                schema: "CoreSchema",
                table: "CompetencyCategoryGradings",
                column: "ReviewTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyCategoryGradings_SoftDeleted",
                schema: "CoreSchema",
                table: "CompetencyCategoryGradings",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyRatingDefinitions_CompetencyId",
                schema: "CoreSchema",
                table: "CompetencyRatingDefinitions",
                column: "CompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyRatingDefinitions_RatingId",
                schema: "CoreSchema",
                table: "CompetencyRatingDefinitions",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyRatingDefinitions_SoftDeleted",
                schema: "CoreSchema",
                table: "CompetencyRatingDefinitions",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyReviewProfiles_SoftDeleted",
                schema: "CoreSchema",
                table: "CompetencyReviewProfiles",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyReviews_CompetencyId",
                schema: "CoreSchema",
                table: "CompetencyReviews",
                column: "CompetencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyReviews_ExpectedRatingId",
                schema: "CoreSchema",
                table: "CompetencyReviews",
                column: "ExpectedRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyReviews_ReviewPeriodId",
                schema: "CoreSchema",
                table: "CompetencyReviews",
                column: "ReviewPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyReviews_ReviewTypeId",
                schema: "CoreSchema",
                table: "CompetencyReviews",
                column: "ReviewTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetencyReviews_SoftDeleted",
                schema: "CoreSchema",
                table: "CompetencyReviews",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Department_DirectorateId",
                schema: "CoreSchema",
                table: "Department",
                column: "DirectorateId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_SoftDeleted",
                schema: "CoreSchema",
                table: "Department",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlans_CompetencyReviewId",
                schema: "CoreSchema",
                table: "DevelopmentPlans",
                column: "CompetencyReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlans_CompetencyReviewProfileId",
                schema: "CoreSchema",
                table: "DevelopmentPlans",
                column: "CompetencyReviewProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DevelopmentPlans_SoftDeleted",
                schema: "CoreSchema",
                table: "DevelopmentPlans",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Directorates_SoftDeleted",
                schema: "CoreSchema",
                table: "Directorates",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_DepartmentId",
                schema: "CoreSchema",
                table: "Divisions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_SoftDeleted",
                schema: "CoreSchema",
                table: "Divisions",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_JobGradeGroups_GroupName",
                schema: "CoreSchema",
                table: "JobGradeGroups",
                column: "GroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobGradeGroups_SoftDeleted",
                schema: "CoreSchema",
                table: "JobGradeGroups",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_JobGrades_GradeCode",
                schema: "CoreSchema",
                table: "JobGrades",
                column: "GradeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobGrades_SoftDeleted",
                schema: "CoreSchema",
                table: "JobGrades",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_JobRoleCompetencies_CompetencyId_JobRoleId_OfficeId",
                schema: "CoreSchema",
                table: "JobRoleCompetencies",
                columns: new[] { "CompetencyId", "JobRoleId", "OfficeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobRoleCompetencies_JobRoleId",
                schema: "CoreSchema",
                table: "JobRoleCompetencies",
                column: "JobRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRoleCompetencies_OfficeId",
                schema: "CoreSchema",
                table: "JobRoleCompetencies",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRoleCompetencies_RatingId",
                schema: "CoreSchema",
                table: "JobRoleCompetencies",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRoleCompetencies_ReviewPeriodId",
                schema: "CoreSchema",
                table: "JobRoleCompetencies",
                column: "ReviewPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRoleCompetencies_SoftDeleted",
                schema: "CoreSchema",
                table: "JobRoleCompetencies",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_JobRoleGrades_JobRoleId",
                schema: "CoreSchema",
                table: "JobRoleGrades",
                column: "JobRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRoleGrades_SoftDeleted",
                schema: "CoreSchema",
                table: "JobRoleGrades",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_JobRoles_SoftDeleted",
                schema: "CoreSchema",
                table: "JobRoles",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeJobRole_JobRoleId",
                schema: "CoreSchema",
                table: "OfficeJobRole",
                column: "JobRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeJobRole_OfficeId",
                schema: "CoreSchema",
                table: "OfficeJobRole",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeJobRole_SoftDeleted",
                schema: "CoreSchema",
                table: "OfficeJobRole",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_DivisionId",
                schema: "CoreSchema",
                table: "Offices",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_SoftDeleted",
                schema: "CoreSchema",
                table: "Offices",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SoftDeleted",
                schema: "CoreSchema",
                table: "Permissions",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Name",
                schema: "CoreSchema",
                table: "Ratings",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_SoftDeleted",
                schema: "CoreSchema",
                table: "Ratings",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewPeriods_BankYearId",
                schema: "CoreSchema",
                table: "ReviewPeriods",
                column: "BankYearId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewPeriods_SoftDeleted",
                schema: "CoreSchema",
                table: "ReviewPeriods",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewTypes_ReviewTypeName",
                schema: "CoreSchema",
                table: "ReviewTypes",
                column: "ReviewTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewTypes_SoftDeleted",
                schema: "CoreSchema",
                table: "ReviewTypes",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "CoreSchema",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "CoreSchema",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_SoftDeleted",
                schema: "CoreSchema",
                table: "RolePermissions",
                column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                        name: "IX_StaffJobRoles_SoftDeleted",
                            schema: "CoreSchema",
                            table: "StaffJobRoles",
                            column: "SoftDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingTypes_SoftDeleted",
                schema: "CoreSchema",
                table: "TrainingTypes",
                column: "SoftDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "AssignJobGradeGroups",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "BehavioralCompetencies",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "CompetencyCategoryGradings",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "CompetencyRatingDefinitions",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "DevelopmentPlans",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "JobRoleCompetencies",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "JobRoleGrades",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "OfficeJobRole",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "StaffJobRoles",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "TrainingTypes",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "JobGrades",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "JobGradeGroups",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "CompetencyReviewProfiles",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "CompetencyReviews",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "JobRoles",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "Offices",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "Competencies",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "Ratings",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "ReviewPeriods",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "ReviewTypes",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "Divisions",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "CompetencyCategories",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "BankYears",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "CoreSchema");

            migrationBuilder.DropTable(
                name: "Directorates",
                schema: "CoreSchema");
        }
    }
}
