using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HanuEdmsApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicYear",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AcademicYearShort = table.Column<string>(maxLength: 3, nullable: false),
                    AcademicYearDescription = table.Column<string>(maxLength: 12, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYear", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Content = table.Column<string>(maxLength: 2048, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourseTypeName = table.Column<string>(maxLength: 30, nullable: false),
                    PricePerCredit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Faculty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FacultyName = table.Column<string>(maxLength: 127, nullable: false),
                    RequiredCreditCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(nullable: false),
                    Dob = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Nationality = table.Column<string>(nullable: false),
                    Hometown = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SemesterName = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 35, nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PeriodStart = table.Column<DateTime>(nullable: false),
                    PeriodEnd = table.Column<DateTime>(nullable: false),
                    SemesterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationPeriod_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ClassName = table.Column<string>(maxLength: 6, nullable: false),
                    PassedCreditCount = table.Column<int>(nullable: false),
                    OverallMark = table.Column<double>(nullable: false),
                    FacultyId = table.Column<int>(nullable: true),
                    AcademicYearId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_AcademicYear_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYear",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Student_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_UserAccount_Id",
                        column: x => x.Id,
                        principalTable: "UserAccount",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_Profile_Id",
                        column: x => x.Id,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourseCode = table.Column<string>(maxLength: 8, nullable: false),
                    CourseName = table.Column<string>(maxLength: 60, nullable: false),
                    CreditCount = table.Column<int>(nullable: false),
                    DifficultyLevel = table.Column<int>(nullable: false),
                    Required = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CourseTypeId = table.Column<int>(nullable: true),
                    FacultyId = table.Column<int>(nullable: true),
                    SemesterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Course_CourseType_CourseTypeId",
                        column: x => x.CourseTypeId,
                        principalTable: "CourseType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_Faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseClass",
                columns: table => new
                {
                    CourseClassId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourseClassCode = table.Column<string>(maxLength: 10, nullable: false),
                    RemainingSlots = table.Column<int>(nullable: false),
                    MaxSlots = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseClass", x => x.CourseClassId);
                    table.ForeignKey(
                        name: "FK_CourseClass_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeReport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AttendanceMark = table.Column<double>(nullable: false),
                    InternalMark = table.Column<double>(nullable: false),
                    FinalMark = table.Column<double>(nullable: false),
                    OverallMark = table.Column<double>(nullable: false),
                    course_id = table.Column<int>(nullable: true),
                    student_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GradeReport_Course_course_id",
                        column: x => x.course_id,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GradeReport_Student_student_id",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timetable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<int>(nullable: false),
                    SessionNo = table.Column<int>(nullable: false),
                    Venue = table.Column<string>(nullable: false),
                    InstructorName = table.Column<string>(nullable: false),
                    IsLecture = table.Column<bool>(nullable: false),
                    CourseClassId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timetable_CourseClass_CourseClassId",
                        column: x => x.CourseClassId,
                        principalTable: "CourseClass",
                        principalColumn: "CourseClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false),
                    StudentId = table.Column<int>(nullable: true),
                    CourseId = table.Column<int>(nullable: true),
                    CourseClassId = table.Column<int>(nullable: true),
                    SemesterId = table.Column<int>(nullable: true),
                    GradesReportId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registration_CourseClass_CourseClassId",
                        column: x => x.CourseClassId,
                        principalTable: "CourseClass",
                        principalColumn: "CourseClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_GradeReport_GradesReportId",
                        column: x => x.GradesReportId,
                        principalTable: "GradeReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registration_Semester_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semester",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registration_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeeLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<bool>(nullable: false),
                    LineSum = table.Column<float>(nullable: false),
                    RegistrationId = table.Column<int>(nullable: true),
                    CourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeeLine_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeeLine_Registration_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_CourseTypeId",
                table: "Course",
                column: "CourseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_FacultyId",
                table: "Course",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_RegistrationPeriodId",
                table: "Course",
                column: "RegistrationPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_SemesterId",
                table: "Course",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClass_CourseId",
                table: "CourseClass",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeLine_CourseId",
                table: "FeeLine",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeLine_RegistrationId",
                table: "FeeLine",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeReport_course_id",
                table: "GradeReport",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_GradeReport_student_id",
                table: "GradeReport",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_CourseClassId",
                table: "Registration",
                column: "CourseClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_CourseId",
                table: "Registration",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_GradesReportId",
                table: "Registration",
                column: "GradesReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_SemesterId",
                table: "Registration",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Registration_StudentId",
                table: "Registration",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationPeriod_SemesterId",
                table: "RegistrationPeriod",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_AcademicYearId",
                table: "Student",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_FacultyId",
                table: "Student",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Timetable_CourseClassId",
                table: "Timetable",
                column: "CourseClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "FeeLine");

            migrationBuilder.DropTable(
                name: "Timetable");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "CourseClass");

            migrationBuilder.DropTable(
                name: "GradeReport");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "CourseType");

            migrationBuilder.DropTable(
                name: "RegistrationPeriod");

            migrationBuilder.DropTable(
                name: "AcademicYear");

            migrationBuilder.DropTable(
                name: "Faculty");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Semester");
        }
    }
}
