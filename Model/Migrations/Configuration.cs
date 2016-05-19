using System.Collections.Generic;
using Model.Entity;

namespace Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Model.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Model.Concrete.EFDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Products.AddOrUpdate(p => p.Name,
                new Product { Name = "Rice", Price = 30.00, Description = "For Eat", Thumbnail = "http://culturalhealthsolutions.com/wp-content/uploads/2015/09/rice.jpg", Tags = new List<string> { "Edible" }, Amount = 50 },
                new Product { Name = "Shampoo", Price = 60.00, Description = "For Use", Thumbnail = "http://ecx.images-amazon.com/images/I/81UIfFxHSKL._SL1500_.jpg", Tags = new List<string> { "Usable" }, Amount = 50 },
                new Product { Name = "Coffee Bean", Price = 500.00, Description = "For Eat", Thumbnail = "http://www.drterlep.com/wp-content/uploads/cup-of-coffee.jpg", Tags = new List<string> { "Edible" }, Amount = 60 },
                new Product { Name = "Rice Berry", Price = 70.00, Description = "For Eat", Thumbnail = "http://mom.girlstalkinsmack.com/image/122012/21/173837834.jpg", Tags = new List<string> { "Edible" }, Amount = 40 },
                new Product { Name = "Pan", Price = 500.00, Description = "For Use", Thumbnail = "http://pngimg.com/upload/frying_pan_PNG8347.png", Tags = new List<string> { "Usable" }, Amount = 70 }
            );
        }
    }
}
