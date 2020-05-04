using HanuEdmsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HanuEdmsApi.EF
{
    public partial class HanuEdmsContext : DbContext
    {
        //static LoggerFactory object
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public HanuEdmsContext()
        {
        }

        public HanuEdmsContext(DbContextOptions<HanuEdmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicYear> AcademicYear { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseClass> CourseClass { get; set; }
        public virtual DbSet<CourseType> CourseType { get; set; }
        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<FeeLine> FeeLine { get; set; }
        public virtual DbSet<Grades> Grades { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<RegistrationPeriod> RegistrationPeriod { get; set; }
        public virtual DbSet<Semester> Semester { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Timetable> Timetable { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(MyLoggerFactory)  //tie-up DbContext with LoggerFactory object
                    .EnableSensitiveDataLogging()
                    .UseMySql("server=localhost;port=3306;user=root;password=root3306;database=hanuedms", x => x.ServerVersion("8.0.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicYear>(entity =>
            {
                entity.ToTable("academic_year");

                entity.Property(e => e.AcademicYearDescription)
                    .IsRequired()
                    .HasColumnType("varchar(12)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.AcademicYearShort)
                    .IsRequired()
                    .HasColumnType("varchar(3)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("article");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.HasIndex(e => e.CourseTypeId)
                    .HasName("IX_Course_CourseTypeId");

                entity.HasIndex(e => e.FacultyId)
                    .HasName("IX_Course_FacultyId");

                entity.HasIndex(e => e.SemesterId)
                    .HasName("IX_Course_SemesterId");

                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasColumnType("varchar(8)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.HasOne(d => d.CourseType)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.CourseTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_CourseType_CourseTypeId");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK_Course_Faculty_FacultyId");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_Course_Semester_SemesterId");
            });

            modelBuilder.Entity<CourseClass>(entity =>
            {
                entity.ToTable("course_class");

                entity.HasIndex(e => e.CourseId)
                    .HasName("IX_CourseClass_CourseId");

                entity.Property(e => e.CourseClassCode)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseClass)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_CourseClass_Course_CourseId");
            });

            modelBuilder.Entity<CourseType>(entity =>
            {
                entity.ToTable("course_type");

                entity.Property(e => e.CourseTypeName)
                    .IsRequired()
                    .HasColumnType("varchar(60)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId)
                    .HasColumnType("varchar(95)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("faculty");

                entity.Property(e => e.FacultyName)
                    .IsRequired()
                    .HasColumnType("varchar(127)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<FeeLine>(entity =>
            {
                entity.ToTable("fee_line");

                entity.HasIndex(e => e.CourseId)
                    .HasName("IX_FeeLine_CourseId");

                entity.HasIndex(e => e.RegistrationId)
                    .HasName("IX_FeeLine_RegistrationId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.FeeLine)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_FeeLine_Course");

                entity.HasOne(d => d.Registration)
                    .WithMany(p => p.FeeLine)
                    .HasForeignKey(d => d.RegistrationId)
                    .HasConstraintName("FK_FeeLine_Registration");
            });

            modelBuilder.Entity<Grades>(entity =>
            {
                entity.ToTable("grades");

                entity.HasIndex(e => e.CourseId)
                    .HasName("IX_GradeReport_course_id");

                entity.HasIndex(e => e.RegistrationId)
                    .HasName("FK_GradeReport_Registration_idx");

                entity.HasIndex(e => e.StudentId)
                    .HasName("IX_GradeReport_student_id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeReport_Course_course_id");

                entity.HasOne(d => d.Registration)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.RegistrationId)
                    .HasConstraintName("FK_GradeReport_Registration");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_GradeReport_Student_student_id");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("profile");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasColumnType("longtext")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Hometown)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Phone)
                    .HasColumnType("longtext")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("registration");

                entity.HasIndex(e => e.CourseClassId)
                    .HasName("IX_Registration_CourseClassId");

                entity.HasIndex(e => e.CourseId)
                    .HasName("IX_Registration_CourseId");

                entity.HasIndex(e => e.SemesterId)
                    .HasName("IX_Registration_SemesterId");

                entity.HasIndex(e => e.StudentId)
                    .HasName("IX_Registration_StudentId");

                entity.HasOne(d => d.CourseClass)
                    .WithMany(p => p.Registration)
                    .HasForeignKey(d => d.CourseClassId)
                    .HasConstraintName("FK_Registration_CourseClass_CourseClassId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Registration)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Registration_Course_CourseId");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Registration)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_Registration_Semester_SemesterId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Registration)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Registration_Student_StudentId");
            });

            modelBuilder.Entity<RegistrationPeriod>(entity =>
            {
                entity.ToTable("registration_period");

                entity.HasIndex(e => e.SemesterId)
                    .HasName("IX_RegistrationPeriod_SemesterId");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.RegistrationPeriod)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_RegistrationPeriod_Semester_SemesterId");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.ToTable("semester");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.SemesterName)
                    .IsRequired()
                    .HasColumnType("longtext")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.HasIndex(e => e.AcademicYearId)
                    .HasName("IX_Student_AcademicYearId");

                entity.HasIndex(e => e.FacultyId)
                    .HasName("IX_Student_FacultyId");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasColumnType("varchar(6)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.AcademicYearId)
                    .HasConstraintName("FK_Student_AcademicYear_AcademicYearId");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Student_Faculty_FacultyId");

                entity.HasOne(d => d.UserAccount)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.Id)
                    .HasConstraintName("FK_Student_UserAccount_Id");

                entity.HasOne(d => d.BasicProfile)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.Id)
                    .HasConstraintName("FK_Student_Profile_Id");
            });

            modelBuilder.Entity<Timetable>(entity =>
            {
                entity.ToTable("timetable");

                entity.HasIndex(e => e.CourseClassId)
                    .HasName("IX_Timetable_CourseClassId");

                entity.Property(e => e.InstructorName)
                    .HasColumnType("longtext")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.IsLecture).HasColumnType("bit(1)");

                entity.Property(e => e.Venue)
                    .HasColumnType("longtext")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.HasOne(d => d.CourseClass)
                    .WithMany(p => p.Timetable)
                    .HasForeignKey(d => d.CourseClassId)
                    .HasConstraintName("FK_Timetable_CourseClass_CourseClassId");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("PRIMARY");

                entity.ToTable("user_account");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(64)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnType("varchar(35)")
                    .HasCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
