using Microsoft.AspNetCore.Identity;
using ReviewService.Infrastructure.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Persistance
{
    public static class ReviewServiceDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");
            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { FullName= "Michael Abrash", UserName = "admin@localhost.com", Email = "admin@localhost.com" };
            if (userManager.Users.All(u => u.Email != administrator.Email))
            {
                await userManager.CreateAsync(administrator, "Admin1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }

            var managerRole = new IdentityRole("Manager");
            if (roleManager.Roles.All(r => r.Name != managerRole.Name))
            {
                await roleManager.CreateAsync(managerRole);
            }

            var count = 0;
            var managers = new List<ApplicationUser>();
            managers.Add(new ApplicationUser { FullName = "Scott Adams", UserName = "manager1@localhost.com", Email = "manager1@localhost.com" });
            managers.Add(new ApplicationUser { FullName = "Tarn Adams", UserName = "manager2@localhost.com", Email = "manager2@localhost.com" });
            foreach (var manager in managers)
            {
                count++;
                if (userManager.Users.All(u => u.UserName != manager.UserName))
                {
                    await userManager.CreateAsync(manager, $"Manager{count}!");
                    await userManager.AddToRolesAsync(manager, new[] { managerRole.Name });
                }
            }

            var reviewerRole = new IdentityRole("Reviewer");
            if (roleManager.Roles.All(r => r.Name != reviewerRole.Name))
            {
                await roleManager.CreateAsync(reviewerRole);
            }

            count = 0;
            var reviewers = new List<ApplicationUser>();
            reviewers.Add(new ApplicationUser { FullName = "Leonard Adleman", UserName = "reviewer1@localhost.com", Email = "reviewer1@localhost.com" });
            reviewers.Add(new ApplicationUser { FullName = "Alfred Aho", UserName = "reviewer2@localhost.com", Email = "reviewer2@localhost.com" });
            reviewers.Add(new ApplicationUser { FullName = "Andrei Alexandrescu", UserName = "reviewer3@localhost.com", Email = "reviewer3@localhost.com" });
            reviewers.Add(new ApplicationUser { FullName = "Paul Allen", UserName = "reviewer4@localhost.com", Email = "reviewer4@localhost.com" });
            foreach(var reviewer in reviewers)
            {
                count++;
                if (userManager.Users.All(u => u.UserName != reviewer.UserName))
                {
                    await userManager.CreateAsync(reviewer, $"Reviewer{count}!");
                    await userManager.AddToRolesAsync(reviewer, new[] { reviewerRole.Name });
                }
            }

            var reviewees = new List<ApplicationUser>();
            reviewees.Add(new ApplicationUser { FullName = "Eric Allman", UserName = "reviewee1@localhost.com", Email = "reviewee1@localhost.com" });
            reviewees.Add(new ApplicationUser { FullName = "Marc Andreessen", UserName = "reviewee2@localhost.com", Email = "reviewee2@localhost.com" });
            reviewees.Add(new ApplicationUser { FullName = "Jeremy Ashkenas", UserName = "reviewee3@localhost.com", Email = "reviewee3@localhost.com" });
            reviewees.Add(new ApplicationUser { FullName = "Bill Atkinson", UserName = "reviewee4@localhost.com", Email = "reviewee4@localhost.com" });
            count = 0;
            foreach (var reviewee in reviewees)
            {
                count++;
                if (userManager.Users.All(u => u.UserName != reviewee.UserName))
                {
                    await userManager.CreateAsync(reviewee, $"Reviewee{count}!");
                }
            }
        }
    }
}
