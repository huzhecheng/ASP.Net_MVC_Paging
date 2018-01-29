using Paging.Interface;
using Paging.Models;
using Paging.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paging.Controllers
{
	public class PageController : Controller
	{
		private DataService service = new DataService();

		public ActionResult PageDemo()
		{	
			var model = new ViewModel
			{
				CustomerItem = service.GetCustomer(1)
			};

			return View(model);
		}

		[HttpPost]
		public ActionResult PageDemo(int Page)
		{
			var model = new ViewModel
			{
				CustomerItem = service.GetCustomer(Page)
			};

			return PartialView("_PartialView", model);
		}
	}

	public class ViewModel
	{
		public IPageList<Customer> CustomerItem { get; set; }
	}
}