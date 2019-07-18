using GoodConvo.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GoodConvo.Data
{
    public class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) 
        {
                
            context.Database.EnsureCreated();

            //Delete users
            /*var me = await userManager.FindByNameAsync("nszeitli@gmail.com");
            await userManager.DeleteAsync(me);

            var shaira = await userManager.FindByNameAsync("azcunasmb@gmail.com");
            await userManager.DeleteAsync(shaira);*/

            //Create roles
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "admin" });
            }
            if (await roleManager.FindByNameAsync("free") == null)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "free" });
            }
            if (await roleManager.FindByNameAsync("paid") == null)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = "paid" });
            }

            //Create roles
            if( await userManager.FindByNameAsync("admin@vacmopscrub.com") == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Nathan",
                    UserName = "admin@vacmopscrub.com",
                    Email = "admin@vacmopscrub.com"
                };
                var result = await userManager.CreateAsync(user);
                if(result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, "!Akarma464");
                    await userManager.AddToRoleAsync(user, "admin");
                }
            }
        }
    }
}
