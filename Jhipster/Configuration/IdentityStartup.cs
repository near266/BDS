using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jhipster.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Jhipster.Configuration
{
    public static class IdentityConfiguration
    {
        public static IApplicationBuilder UseApplicationIdentity(this IApplicationBuilder builder,
            IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                SeedRoles(roleManager).Wait();
                SeedUsers(userManager).Wait();
                SeedUserRoles(userManager).Wait();
            }

            return builder;
        }


        private static IEnumerable<Role> Roles()
        {
            return new List<Role> {
                new Role {Id = "role_admin", Name = "ROLE_ADMIN"},
                new Role {Id = "role_user",Name = "ROLE_USER"}
            };
        }

        private static IEnumerable<User> Users()
        {
            return new List<User> {
                new User {
                    Id = "e7502689-bec6-4462-beaf-9368d7a441e4",
                    UserName = "system",
                    PasswordHash = "$2a$10$mE.qmcV0mFU5NcKh73TZx.z4ueI/.bDWbj0T1BYyqP481kGGarKLG",
                    FirstName = "",
                    LastName = "System",
                    Email = "system@localhost",
                    Activated = true,
                    LangKey = "en"
                },
                new User {
                    Id = "0228b8ca-66a2-4d91-b0cb-305e11f7803c",
                    UserName = "anonymoususer",
                    PasswordHash = "$2a$10$j8S5d7Sr7.8VTOYNviDPOeWX8KcYILUVJBsYV83Y5NtECayypx9lO",
                    FirstName = "Anonymous",
                    LastName = "User",
                    Email = "anonymous@localhost",
                    Activated = true,
                    LangKey = "en"
                },
                new User {
                    Id = "13347e15-8b5d-49a7-b7bf-c9f073e97967",
                    UserName = "admin",
                    PasswordHash = "$2a$10$gSAhZrxMllrbgj/kkK9UceBPpChGWJA7SYIb1Mqo.n5aNLq1/oRrC",
                    FirstName = "admin",
                    LastName = "Administrator",
                    Email = "admin@localhost",
                    Activated = true,
                    LangKey = "en"
                },
                new User {
                    Id = "56bea1ab-c711-47ee-9362-8cef4ed817bf",
                    UserName = "xuanadmin",
                    PasswordHash = "$2a$10$gSAhZrxMllrbgj/kkK9UceBPpChGWJA7SYIb1Mqo.n5aNLq1/oRrC",
                    FirstName = "Doãn Thị Xuân",
                    LastName = "Administrator",
                    Email = "admin-xuan@gmail.com",
                    Activated = true,
                    LangKey = "en"
                },
                new User {
                    Id = "adeaac3a-a71e-4ed8-86d9-3a1f898fe223",
                    UserName = "user",
                    PasswordHash = "$2a$10$VEjxo0jq2YG9Rbk2HmX9S.k1uZBGYUHdUcid3g/vfiEl7lwWgOH/K",
                    FirstName = "",
                    LastName = "User",
                    Email = "user@localhost",
                    Activated = true,
                    LangKey = "en"
                }
            };
        }

        private static IDictionary<string, string[]> UserRoles()
        {
            return new Dictionary<string, string[]> {
                {"e7502689-bec6-4462-beaf-9368d7a441e4", new[] {"ROLE_ADMIN", "ROLE_USER"}},
                {"13347e15-8b5d-49a7-b7bf-c9f073e97967", new[] {"ROLE_ADMIN", "ROLE_USER"}},
                {"56bea1ab-c711-47ee-9362-8cef4ed817bf", new[] {"ROLE_ADMIN", "ROLE_USER"}},
                {"adeaac3a-a71e-4ed8-86d9-3a1f898fe223", new[] {"ROLE_USER"}}
            };
        }

        private static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            foreach (var role in Roles())
            {
                var dbRole = await roleManager.FindByNameAsync(role.Name);
                if (dbRole == null)
                {
                    await roleManager.CreateAsync(role);
                }
                else
                {
                    await roleManager.UpdateAsync(dbRole);
                }
            }
        }

        private static async Task SeedUsers(UserManager<User> userManager)
        {
            foreach (var user in Users())
            {
                var dbUser = await userManager.FindByIdAsync(user.Id);
                if (dbUser == null)
                {
                    await userManager.CreateAsync(user);
                }
                else
                {
                    await userManager.UpdateAsync(dbUser);
                }
            }
        }

        private static async Task SeedUserRoles(UserManager<User> userManager)
        {
            foreach (var (id, roles) in UserRoles())
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.AddToRolesAsync(user, roles);
            }
        }
    }
}
