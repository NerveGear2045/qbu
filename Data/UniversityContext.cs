using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using qbu.Models;

namespace qbu.Data;

public partial class UniversityContext : DbContext
{
    public UniversityContext()
    {
    }

    public UniversityContext(DbContextOptions<UniversityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Lecturer> Lecturers { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasOne(d => d.Lecturer).WithMany(p => p.Courses).HasConstraintName("FK_Course_Lecturer");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.Property(e => e.Grade1).IsFixedLength();

            entity.HasOne(d => d.Course).WithMany()
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Grade_Course");

            entity.HasOne(d => d.Student).WithMany().HasConstraintName("FK_Grade_Student");
        });

        modelBuilder.Entity<Lecturer>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Lecturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lecturer_Account");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Phone).IsFixedLength();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Student)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
