using LookupInvoice.Domain;
using LookupInvoice.Domain.Infrastructure.Abstract;
using LookupInvoice.Domain.Utility;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LookupInvoice.Controllers
{
    public class TraCuuController : Controller
    {
        // GET: TraCuu
        private readonly IRepository<DulieuHoadon> _repository;

        public TraCuuController(IRepository<DulieuHoadon> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult GetInvoice(string maBaoMat, string maSoThue)
        {
            var dbContext = (Entities)ObjectFactory.GetInstance<IDbFactory>();
            var connection = ConfigurationManager.ConnectionStrings["Entities"];
            var ecsBuilder = new EntityConnectionStringBuilder(connection.ConnectionString);
            var sqlBuilder = new SqlConnectionStringBuilder(ecsBuilder.ProviderConnectionString) { InitialCatalog = maSoThue };
            if (dbContext.Database.Connection.State == ConnectionState.Open)
                dbContext.Database.Connection.Close();
            dbContext.Database.Connection.ConnectionString = sqlBuilder.ConnectionString;

            var invoice = _repository.GetAll();
            var selectedInvoice = invoice.SingleOrDefault(x => x.Mabaomat == maBaoMat);
            if (selectedInvoice == null)
            {
                return Content("Item not found");
            }
            var firstPara = maSoThue;
            var kyhieu = selectedInvoice.Kyhieu;
            var secondPara = kyhieu.Remove(2, 1);
            var sohoadon = selectedInvoice.Sohoadon.ToString();
            var url = "http://hoadondientu.link/" + firstPara + "/" + firstPara + "_" + secondPara + "_" + sohoadon + ".xml";
            return RedirectToAction("Index", "Result", new { url = url });
        }

        public ActionResult Index()
        {
            return View();
        }
    }

    //public class MultiTenantFilter : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //    }
    //}
}