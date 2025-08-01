using SmartCareerPlatform;
using SmartCareerPlatform.Models;
using SmartCareerPlatform.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using SmartCareerPlatform.Server.Data; 

namespace SmartCareerPlatform.Server.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
         
            context.Database.EnsureCreated();

           
            if (!context.Users.Any())
            {
                var hashedPassword = HashPassword("password");
                context.Users.Add(new User { Username = "test", PasswordHash = hashedPassword, Email = "test@example.com", Experience = 2 });
                context.SaveChanges();
            }

            // Seed courses using the CourseSeeder
            CourseSeeder.SeedCourses(context);

           
            if (!context.Skills.Any())
            {
                context.Skills.Add(new Skill { Name = "Machine Learning" });
                context.Skills.Add(new Skill { Name = "AI" });
                context.Skills.Add(new Skill { Name = "HTML" });
                context.Skills.Add(new Skill { Name = "CSS" });
                context.Skills.Add(new Skill { Name = "JavaScript" });
                context.SaveChanges();
            }
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
