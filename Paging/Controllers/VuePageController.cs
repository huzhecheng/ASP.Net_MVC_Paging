using Paging.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paging.Controllers
{
	public class VuePageController : Controller
	{
		private DataService service = new DataService();

		// GET: VuePage
		public ActionResult VuePageDemo()
		{
			return View();
		}

		[HttpPost]
		public JsonResult GetCustomer(int Page)
		{
			var data = service.GetData(Page);

			return Json(new { data = data.Item1, pageinfo = data.Item2 });
		}
	}
}