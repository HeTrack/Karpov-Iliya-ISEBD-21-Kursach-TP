using Microsoft.EntityFrameworkCore;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement
{
    public class UniversityDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=UniversityDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Course> Courses { set; get; }
        public virtual DbSet<Education> Educations { set; get; }
        public virtual DbSet<EducationCourse> EducationCourses { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Pay> Pays { set; get; }
    }
}
