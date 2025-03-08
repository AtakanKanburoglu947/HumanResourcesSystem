using HumanResourcesSystemCore.AuthModels;
using HumanResourcesSystemCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourcesSystemRepository
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set;}
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<TrainingRecord> TrainingRecords { get; set;}
        public DbSet<WorkReport> WorkReports { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<DatabaseLog> DatabaseLogs { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                       .HasOne(u => u.Manager)
                       .WithMany()
                       .HasForeignKey(u => u.ManagerId)
                       .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RefreshToken>().HasOne(u => u.User)
                .WithMany(u=> u.RefreshTokens)
                .HasForeignKey(u => u.UserId);
            builder.Entity<LeaveRequest>()
                .HasOne(l => l.User)
                .WithMany(u => u.LeaveRequests)
                .HasForeignKey(l => l.UserId);

            builder.Entity<LeaveRequest>()
                .HasOne(l => l.Manager)
                .WithMany()
                .HasForeignKey(l => l.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PerformanceReview>()
                .HasOne(p => p.User)
                .WithMany(u => u.PerformanceReviews)
                .HasForeignKey(p => p.UserId);

            builder.Entity<PerformanceReview>()
                .HasOne(p => p.Reviewer)
                .WithMany()
                .HasForeignKey(p => p.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<JobApplication>()
                .HasOne(j => j.Reviewer)
                .WithMany()
                .HasForeignKey(j => j.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
    .HasOne(u => u.Department)
    .WithMany(d => d.Users)
    .HasForeignKey(u => u.DepartmentId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .Property(u => u.Salary)
                .HasPrecision(18, 2);

        }
    }
}
