using LowCostFligtsBrowser.Domain.Entities;
using LowCostFligtsBrowser.Domain.ValueObjects;
using LowCostFligtsBrowser.Infrastructure.Files;
using LowCostFligtsBrowser.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.TodoLists.Any())
            {
                context.TodoLists.Add(new TodoList
                {
                    Title = "Shopping",
                    Colour = Colour.Blue,
                    Items =
                    {
                        new TodoItem { Title = "Apples", Done = true,Status=Domain.Common.SoftDeleteStatus.Active },
                        new TodoItem { Title = "Milk", Done = true,Status=Domain.Common.SoftDeleteStatus.Active },
                        new TodoItem { Title = "Bread", Done = true,Status=Domain.Common.SoftDeleteStatus.Active },
                        new TodoItem { Title = "Toilet paper",Status=Domain.Common.SoftDeleteStatus.Active },
                        new TodoItem { Title = "Pasta",Status=Domain.Common.SoftDeleteStatus.Active },
                        new TodoItem { Title = "Tissues",Status=Domain.Common.SoftDeleteStatus.Active },
                        new TodoItem { Title = "Tuna",Status=Domain.Common.SoftDeleteStatus.Active },
                        new TodoItem { Title = "Water",Status=Domain.Common.SoftDeleteStatus.Active }
                    }
                });

                
            }
            if (!context.Airports.Any())
            {
                // In-Memory DB does NOT support transacitons!!
                context.Database.BeginTransaction();
                context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT Airport ON;");

                try
                {
                    var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
                    var directoryInfo = new FileInfo(location.AbsolutePath).Directory.FullName;
                    string filePath = directoryInfo + $@"\Persistence\ImportDataFiles\ImportAirportsData.csv";
                    var entries = (List<Airport>)CsvFileBuilder.ReadInCSV<Airport>(filePath);
                    if (entries.Count() > 0)
                    {
                        context.Airports.AddRange(entries);
                        await context.SaveChangesAsync();
                        context.Database.CommitTransaction();
                    }
                }
                catch (Exception)
                {
                    context.Database.RollbackTransaction();
                    throw;
                }
                finally
                {
                    context.Database.BeginTransaction();
                    context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT Airport OFF;");
                    context.Database.CommitTransaction();
                }
            }
            
            
        }
    }
}
