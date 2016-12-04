using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Myvas.AspNetCore.TodoApi.Models
{
    public static class ApplicationDbInitializer
    {
        /// <summary>
        /// 往数据库中填充必要的初始化数据
        /// </summary>
        /// <param name="app"></param>
        public static void SeedApplicationDb(this IApplicationBuilder app)
        {
            app.ApplicationServices.SeedData();
        }

        public static void SeedData(this IServiceProvider serviceProvider)
        {
            var todoItemsInitial = new[] {
               new TodoItem {
                    Id = Guid.NewGuid().ToString(),
                     Name = "Item1"
               }
            };

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                context.Database.EnsureCreated();
                {
                    context.Database.Migrate();
                    if (!context.TodoItems.Any())
                    {
                        context.TodoItems.AddRange(todoItemsInitial);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
