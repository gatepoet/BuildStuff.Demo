using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Migrations;

namespace Core.Data
{
    public class DemoContext : DbContext
    {

        static DemoContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DemoContext, Configuration>());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties()
                .Where(p => p.Name == "Id")
                .Configure(p => p.IsKey());
        }
    }

    public class ShoppingCartItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }

        public decimal GetTotal()
        {
            return Product.Price*(decimal) Amount;
        }
    }
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }

        public string Id { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public void Add(Product product, int amount)
        {
            var item = Items.SingleOrDefault(i => i.Product == product);
            if (item == null)
            {
                Items.Add(new ShoppingCartItem{Id = Guid.NewGuid(), Product = product, Amount = amount});
            }
            else
            {
                item.Amount += amount;
            }

        }

        public decimal GetTotal()
        {
            var total = Items.Sum(item => item.GetTotal());

            return total;
        }

        public void UpdateItem(int productId, int amount)
        {
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public ProductCategory Category { get; set; }
    }

    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
