using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Core.Data;
using Web.Models;

namespace Web.Controllers
{
    public class ProductsController : ApiController
    {
        private DemoContext Context { get { return new DemoContext(); } }

        public Product[] Get()
        {
            return Context.Products.ToArray();
        }

        public Product[] ByCategory(string category)
        {
            return Context.Categories.Single(c => c.Name == category).Products.ToArray();
        }

        public Product Get(int id)
        {
            return Context.Products.Single(p => p.Id == id);
        }

        public void Post(CreateProductCommand command)
        {
            var context = Context;
            var category = context.Categories.Single(c => c.Id == command.CategoryId);
            context.Products.Add(new Product {Category = category, Name = command.Name, Price = command.Price});
            context.SaveChanges();
        }
    }
}