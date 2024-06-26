﻿using Microsoft.AspNetCore.Identity;
using MyMvcProject.Models;

namespace MyMvcProject.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            await SeedDefaultUsers(serviceProvider);
            //if (context.Guests.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //var guests = new Guest[]
            //{
            //    new Guest{FirstName="Carson",LastName="Alexander"},
            //    new Guest{FirstName="Meredith",LastName="Alonso"},
            //    new Guest{FirstName="Arturo",LastName="Anand" },
            //    new Guest{FirstName="Gytis",LastName="Barzdukas"},
            //    new Guest{FirstName="Yan",LastName="Li"},
            //    new Guest{FirstName="Peggy",LastName="Justice"},
            //};

            //context.Guests.AddRange(guests);
            //context.SaveChanges();



            if (context.Parties.Any())
            {
                return;   // DB has been seeded
            }

            var parties = new List<Party>();
            for (int i = 0; i < 20; i++)
            {
                parties.Add(new Party()
                {
                    Name = $"Big Party {i}",
                    ImageFileName = "party.jpg",
                    Description = "Description of the massive party. Description of the massive party.Description of the massive party.Description of the massive party.",
                    Created = DateTime.Now
                });
            }

            context.Parties.AddRange(parties);
            context.SaveChanges();
        }

        private static async Task SeedDefaultUsers(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (_roleManager != null)
            {
                IdentityRole? role = await _roleManager.FindByNameAsync("Admin");
                if (role == null)
                {
                    var results = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var me = await _userManager.FindByEmailAsync("barbary@hotmail.com");

                if (me == null)
                {
                    //a hasher to hash the password before seeding the user to the db
                    var hasher = new PasswordHasher<ApplicationUser>();

                    me = new ApplicationUser
                    {
                        Id = "13457890-1937-421b-8e0e-6001d8785140", // primary key
                        UserName = "test@hotmail.com",
                        NormalizedUserName = "TEST@HOTMAIL.COM",
                        Email = "test@hotmail.com",
                        NormalizedEmail = "TEST@HOTMAIL.COM",
                        PasswordHash = hasher.HashPassword(null, "123.Donkey")
                    };
                    var results = await _userManager.CreateAsync(me);
                }

                await _userManager.AddToRoleAsync(me, "Admin");
            }
        }
    }
}
