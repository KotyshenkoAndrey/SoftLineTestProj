using Microsoft.EntityFrameworkCore;
using SoftLineTestProj.Database.Entities;
using System;

namespace SoftLineTestProj.Database
{
    public static class DbInit
    {
        public static void Execute(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
            ArgumentNullException.ThrowIfNull(scope);

            var context = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            ArgumentNullException.ThrowIfNull(context);

            var ii = context.Database.CanConnect();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Status.Any())
                return;
            var a1 = new Status()
            {
                Status_name = "Создана",
            };
            context.Status.Add(a1);
            var a2 = new Status()
            {
                Status_name = "В работе",
            };
            context.Status.Add(a2);
            var a3 = new Status()
            {
                Status_name = "Завершена",
            };
            context.Status.Add(a3);
            context.SaveChanges();
        }
    }

}
