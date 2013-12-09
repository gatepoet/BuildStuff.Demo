using System;
using System.Linq;
using System.Web.Http;
using Core.Data;
using Web.Models;

namespace Web.Controllers
{
    public class ShoppingCartController : ApiController
    {
        private DemoContext Context { get { return new DemoContext(); } }

        public Cart Get(string id)
        {
            var context = Context;
            var cart = context.ShoppingCarts
                .Include("Items")
                .Include("Items.Product")
                .SingleOrDefault(c => c.Id == id);
            if (cart == null)
            {
                cart = new ShoppingCart {Id = id};
                context.ShoppingCarts.Add(cart);
                context.SaveChanges();
            }
            return new Cart
            {
                Id = cart.Id,
                ItemCount = cart.Items.Sum(i => i.Amount),
                Items = cart.Items.Select(i => new{ i.Product.Name, i.Amount, i.Product.Price, Total=i.GetTotal() }).ToArray(),
                Total=cart.GetTotal()
            };
        }

        public void Put(string id, AddItemCommand command)
        {
            var context = Context;
            var cart = context.ShoppingCarts
                .Include("Items")
                .Single(c => c.Id == id);
            var product = context.Products.Single(p => p.Id == command.ProductId);
            cart.Add(product, command.Amount);
            context.SaveChanges();
        }

        public void Post(string id, AddItemCommand command)
        {
            var context = Context;
            var cart = context.ShoppingCarts
                .Include("Items")
                .Single(c => c.Id == id);
            var product = context.Products.Single(p => p.Id == command.ProductId);
            cart.Add(product, command.Amount);
            context.SaveChanges();
        }
        public void Delete(string id, int productId)
        {
            var context = Context;
            var cart = context.ShoppingCarts
                .Include("Items")
                .Include("Items.Product")
                .Single(c => c.Id == id);
            cart.Items.RemoveAll(i => i.Product.Id == productId);
            context.SaveChanges();
        }
    }

    public class Cart
    {
        public string Id { get; set; }
        public dynamic[] Items { get; set; }
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
    }
}