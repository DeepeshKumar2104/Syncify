using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Syncify.Domain.AwsEntities;
namespace Syncify.Infrastructure.Persistence.AwsDB;

public partial class AwsDbContext : DbContext
{
    public AwsDbContext()
    {
    }

    public AwsDbContext(DbContextOptions<AwsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeProject> EmployeeProjects { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:aws-sql-source-server.database.windows.net,1433;Initial Catalog=AWS_Sql_Source;User ID=aws;Password=root@123;Encrypt=True;TrustServerCertificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Addresse__091C2A1BB02302A0");

            entity.Property(e => e.AddressId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("AddressID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Line1).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(10);

            entity.HasOne(d => d.Employee).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Addresses__Emplo__10566F31");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__5E5499A827C9F6C2");

            entity.Property(e => e.LogId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("LogID");
            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AuditLogs__Emplo__18EBB532");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__Contacts__5C6625BB5808B779");

            entity.Property(e => e.ContactId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ContactID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(d => d.Employee).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Contacts__Employ__0C85DE4D");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD8B2D7762");

            entity.HasIndex(e => e.Name, "UQ__Departme__737584F61E7561FD").IsUnique();

            entity.Property(e => e.DepartmentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.HasKey(e => e.DesignationId).HasName("PK__Designat__BABD603E88FBCF17");

            entity.HasIndex(e => e.Title, "UQ__Designat__2CB664DCB8FCA1E7").IsUnique();

            entity.Property(e => e.DesignationId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("DesignationID");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF14F71E949");

            entity.HasIndex(e => e.ExternalEmployeeCode, "UQ__Employee__4ADF49B06FC582F9").IsUnique();

            entity.Property(e => e.EmployeeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("EmployeeID");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DesignationId).HasColumnName("DesignationID");
            entity.Property(e => e.ExternalEmployeeCode).HasMaxLength(20);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employees__Depar__07C12930");

            entity.HasOne(d => d.Designation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DesignationId)
                .HasConstraintName("FK__Employees__Desig__08B54D69");
        });

        modelBuilder.Entity<EmployeeProject>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.ProjectId }).HasName("PK__Employee__6DB1E41C53F79932");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.AssignedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeProjects)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeP__Emplo__1332DBDC");

            entity.HasOne(d => d.Project).WithMany(p => p.EmployeeProjects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeP__Proje__14270015");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABED0BEC404FF");

            entity.HasIndex(e => e.Name, "UQ__Projects__737584F6881B5D3D").IsUnique();

            entity.Property(e => e.ProjectId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ProjectID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
