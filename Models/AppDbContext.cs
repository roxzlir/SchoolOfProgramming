using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolOfProgramming.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EnrollmentList> EnrollmentLists { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<GradeList> GradeLists { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=SchoolOfProgramming;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.Property(e => e.ClassId)
                .ValueGeneratedNever()
                .HasColumnName("ClassID");
            entity.Property(e => e.ClassName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseId)
                .ValueGeneratedNever()
                .HasColumnName("CourseID");
            entity.Property(e => e.SubjectName).HasMaxLength(30);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmpFirstName).HasMaxLength(50);
            entity.Property(e => e.EmpLastName).HasMaxLength(50);
            entity.Property(e => e.FkProfessionId).HasColumnName("FK_ProfessionID");
            entity.Property(e => e.HiredDate).HasColumnType("date");

            entity.HasOne(d => d.FkProfession).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkProfessionId)
                .HasConstraintName("FK_Employees_Professions");
        });

        modelBuilder.Entity<EnrollmentList>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId);

            entity.ToTable("EnrollmentList");

            entity.Property(e => e.EnrollmentId)
                .ValueGeneratedNever()
                .HasColumnName("EnrollmentID");
            entity.Property(e => e.FkClassId).HasColumnName("FK_ClassID");
            entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseID");
            entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeID");
            entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentID");

            entity.HasOne(d => d.FkClass).WithMany(p => p.EnrollmentLists)
                .HasForeignKey(d => d.FkClassId)
                .HasConstraintName("FK_EnrollmentList_Classes");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.EnrollmentLists)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK_EnrollmentList_Courses");

            entity.HasOne(d => d.FkEmployee).WithMany(p => p.EnrollmentLists)
                .HasForeignKey(d => d.FkEmployeeId)
                .HasConstraintName("FK_EnrollmentList_Employees");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.EnrollmentLists)
                .HasForeignKey(d => d.FkStudentId)
                .HasConstraintName("FK_EnrollmentList_Students");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.Property(e => e.GradeId)
                .ValueGeneratedNever()
                .HasColumnName("GradeID");
            entity.Property(e => e.Grade1).HasColumnName("Grade");
        });

        modelBuilder.Entity<GradeList>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("GradeList");

            entity.Property(e => e.FkEnrollmentId).HasColumnName("FK_EnrollmentID");
            entity.Property(e => e.FkGradeId).HasColumnName("FK_GradeID");
            entity.Property(e => e.GradeDate).HasColumnType("date");

            entity.HasOne(d => d.FkEnrollment).WithMany()
                .HasForeignKey(d => d.FkEnrollmentId)
                .HasConstraintName("FK_GradeList_EnrollmentList");

            entity.HasOne(d => d.FkGrade).WithMany()
                .HasForeignKey(d => d.FkGradeId)
                .HasConstraintName("FK_GradeList_Grades");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.Property(e => e.ProfessionId)
                .ValueGeneratedNever()
                .HasColumnName("ProfessionID");
            entity.Property(e => e.ProTitle).HasMaxLength(20);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.StuFirstName).HasMaxLength(50);
            entity.Property(e => e.StuLastName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
