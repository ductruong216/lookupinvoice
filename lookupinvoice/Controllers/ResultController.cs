using System.Web.Mvc;

namespace LookupInvoice.Controllers
{
    public class ResultController : Controller
    {
        // GET: Result
        public ActionResult Index(string url)
        {
            ViewBag.data = url;
            return View();
        }
    }
}