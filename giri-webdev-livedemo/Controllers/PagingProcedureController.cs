using giri_webdev_livedemo.DAL;
using giri_webdev_livedemo.ViewModels;
using System.Web.Mvc;

namespace giri_webdev_livedemo.Controllers
{
    public class PagingProcedureController : Controller
    {
        private PagingProcedureDAL pagingDAL;

        public PagingProcedureController()
        {
            pagingDAL = new PagingProcedureDAL();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        public ActionResult Index(int pageIndex, int pageSize, string sortOrder, string sortColumn)
        {
           ProductViewModel viewModel= pagingDAL.GetProducts(pageIndex, pageSize, sortOrder, sortColumn);

            return View(viewModel);
        }
    }
}