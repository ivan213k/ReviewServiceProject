using Microsoft.AspNetCore.Identity;
using ReviewService.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Infrastructure.Persistance
{
    public static class ReviewServiceDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager)
        {
            var administrator = new ApplicationUser { FullName="Andrii", UserName = "Admin", Email = "admin@localhost.com" };
            if (userManager.Users.All(u => u.Email != administrator.Email))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
            }

            var count = 0;
            var reviewers = new List<ApplicationUser>();
            reviewers.Add(new ApplicationUser { FullName = "Rob", UserName = "reviewer1", Email = "reviewer1@localhost.com" });
            reviewers.Add(new ApplicationUser { FullName = "Ihor", UserName = "reviewer2", Email = "reviewer2@localhost.com" });
            foreach(var reviewer in reviewers)
            {
                count++;
                if (userManager.Users.All(u => u.Email != reviewer.Email))
                {
                    await userManager.CreateAsync(reviewer, $"Reviewer{count}!");
                }
            }

            var reviewees = new List<ApplicationUser>();
            reviewees.Add(new ApplicationUser { FullName = "Pasha", UserName = "reviewee1", Email = "reviewee1@localhost.com" });
            reviewees.Add(new ApplicationUser { FullName = "Bob", UserName = "reviewee2", Email = "reviewee2@localhost.com" });
            reviewees.Add(new ApplicationUser { FullName = "John", UserName = "reviewee3", Email = "reviewee3@localhost.com" });
            reviewees.Add(new ApplicationUser { FullName = "Lilia", UserName = "reviewee4", Email = "reviewee4@localhost.com" });
            reviewees.Add(new ApplicationUser { FullName = "John", UserName = "reviewee5", Email = "reviewee5@localhost.com" });
            reviewees.Add(new ApplicationUser { FullName = "liza", UserName = "reviewee6", Email = "reviewee6@localhost.com" });
            count = 0;
            foreach (var reviewee in reviewees)
            {
                count++;
                if (userManager.Users.All(u => u.Email != reviewee.Email))
                {
                    await userManager.CreateAsync(reviewee, $"Reviewee{count}!");
                }
            }
        }
    }
}
