using System.Web.Mvc;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Load(string id)
        {
            Session["CartId"] = id;
            return new JsonResult();
        }
    }
}