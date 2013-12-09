using System.Linq;
using System.Web.Http;
using Core.Data;
using Web.Models;

namespace Web.Controllers
{
    public class CategoryController : ApiController
    {
        private DemoContext Context { get { return new DemoContext(); } }

        public ProductCategory[] Get()
        {
            return Context.Categories.ToArray();
        }

        public void Put(CreateCateogryCommand command)
        {
            var context =  Context;
            context.Categories.Add(new ProductCategory {Name = command.Name});
            context.SaveChanges();
        }
    }
}