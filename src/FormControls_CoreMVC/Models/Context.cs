using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FormControls_CoreMVC.Models
{
    public class FormsDbContext : DbContext
    {
        public FormsDbContext(DbContextOptions<FormsDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserCountry> UserCountries { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<UserDescription> UserDescriptions { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Upload_Core;Trusted_Connection=True;");
        //}
        // OR Add Below code to Startup.cs ConfigureServices
        //var connection = @"Server=(LocalDb)\MSSQLLocalDB;Database=FormControls;Trusted_Connection=True;";
        //services.AddDbContext<FormsDbContext>(options => options.UseSqlServer(connection));
        
    }
    public static class SeedData
    {
        public static void Seed(this IApplicationBuilder app)
        {
            //Add app.Seed(); to Startup.cs Configure.

            var context = app.ApplicationServices.GetService<FormsDbContext>();

            context.Countries.Add(new Country { ID = "1", Name = "Australia" });
            context.Countries.Add(new Country { ID = "2", Name = "Canada" });
            context.Countries.Add(new Country { ID = "3", Name = "Turkey" });
            context.Countries.Add(new Country { ID = "4", Name = "United Kingdom" });
            context.Countries.Add(new Country { ID = "5", Name = "United States" });

            context.Courses.Add(new Course { Name = "Course 1", ID = "1", Checked = false });
            context.Courses.Add(new Course { Name = "Course 2", ID = "2", Checked = false });
            context.Courses.Add(new Course { Name = "Course 3", ID = "3", Checked = false });
            context.Courses.Add(new Course { Name = "Course 4", ID = "4", Checked = false });
            context.Courses.Add(new Course { Name = "Course 5", ID = "5", Checked = false });

            context.SaveChanges();
        }
    }
}
