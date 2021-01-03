using Microsoft.AspNetCore.Identity;
using Shop.Database.Identity.Entities;
using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Database
{
    public class DatabaseInitializer
    {

        public void Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = Enum.GetValues(typeof(AccessCode));

            foreach (var role in roles)
            {

                if (!roleManager.RoleExistsAsync(role.ToString()).Result)
                {
                    var createRoleResult = roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString()
                    }).Result;
                }
            }

            var mobinUser = userManager.FindByNameAsync("09197442364").Result;
            var mahdiUser = userManager.FindByNameAsync("09212651629").Result;
            var mahdiUser1 = userManager.FindByNameAsync("09197572162").Result;
            if (mobinUser == null)
            {
                var result = userManager.CreateAsync(new User
                {
                    UserName = "09197442364",
                    PhoneNumber = "09197442364",
                    FullName = "مبین حسنی",
                    Type = UserType.Programmer,
                    RegisterDate = DateTime.Now,
                }, "9197442364"
                 ).Result;
            }
            if (mahdiUser == null)
            {
                var result = userManager.CreateAsync(new User
                {
                    UserName = "09212651629",
                    PhoneNumber = "09212651629",
                    FullName = "مهدی حسنی",
                    Type = UserType.Programmer,
                    RegisterDate = DateTime.Now,
                }, "9197572162"
                 ).Result;
            }
            if (mahdiUser1 == null)
            {
                var result = userManager.CreateAsync(new User
                {
                    UserName = "09197572162",
                    PhoneNumber = "09197572162",
                    FullName = "مهدی حسنی",
                    Type = UserType.Programmer,
                    RegisterDate = DateTime.Now,
                }, "9197572162"
                 ).Result;
            }
        }
    }
}
