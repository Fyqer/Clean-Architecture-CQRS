using Webinar202103.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Task = Webinar202103.Domain.Entities.Task;

namespace Webinar202103.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async System.Threading.Tasks.Task SeedDefaultUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new IdentityUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        public static async System.Threading.Tasks.Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.TaskLists.Any())
            {
                context.TaskLists.Add(new TaskList
                {
                    Title = "Shopping",
                    Items =
                    {
                        new Task { Title = "Apples" },
                        new Task { Title = "Milk" },
                        new Task { Title = "Bread" },
                        new Task { Title = "Toilet paper" },
                        new Task { Title = "Pasta" },
                        new Task { Title = "Tissues" },
                        new Task { Title = "Tuna" },
                        new Task { Title = "Water" }
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
