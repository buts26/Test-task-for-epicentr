using Microsoft.EntityFrameworkCore;
using ProjectManagement.EntityFramework.Shared.Entities;
using ProjectManagement.EntityFramework.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ProjectManagement.EntityFramework.Shared.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProjectType>()
                .Property(e => e.ProjectTypeId)
                .HasConversion<int>();

            modelBuilder
                .Entity<ProjectType>().HasData(
                    Enum.GetValues(typeof(ProjectTypeEnum))
                        .Cast<ProjectTypeEnum>()
                        .Select(e => new ProjectType()
                        {
                            ProjectTypeId = e,
                            Name = e.ToString()
                        })
                );
        }
    }
}
