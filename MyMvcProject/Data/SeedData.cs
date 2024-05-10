using Microsoft.AspNetCore.Identity;
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
            var events = new Party[]
                {
                    new Party()
                    {
                        Name = "Big Party 1",
                        ImageFileName = "party.jpg",
                        Description = "Description of the massive party. Description of the massive party.Description of the massive party.Description of the massive party.",
                        Created = DateTime.Now
                    },
                    new Party()
                    {
                        Name = "Big Party 2",
                        ImageFileName = "party.jpg",
                        Description = "Description of the massive party. Description of the massive party.Description of the massive party.Description of the massive party.",
                        Created = DateTime.Now
                    },
                    new Party()
                    {
                        Name = "Big Party 3",
                        ImageFileName = "party.jpg",
                        Description = "Description of the massive party. Description of the massive party.Description of the massive party.Description of the massive party.",
                        Created = DateTime.Now
                    },
                };

            context.Parties.AddRange(events);
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
                        Id = "23457890-1937-421b-8e0e-6001d8785140", // primary key
                        UserName = "barbary@hotmail.com",
                        NormalizedUserName = "BARBARY@HOTMAIL.COM",
                        Email = "barbary@hotmail.com",
                        NormalizedEmail = "BARBARY@HOTMAIL.COM",
                        PasswordHash = hasher.HashPassword(null, "123.Donkey")
                    };
                    var results = await _userManager.CreateAsync(me);
                }

                await _userManager.AddToRoleAsync(me, "Admin");
            }
        }
    }
}
