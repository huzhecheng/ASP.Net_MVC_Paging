using Paging.Extention;
using Paging.Interface;
using Paging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paging.Controllers
{
	public class PageController : Controller
	{
		private Service dataservice = new Service();

		public ActionResult PageDemo()
		{	
			var model = new ViewModel
			{
				CustomerItem = dataservice.Fun_GCustomer(1, 10)
			};

			return View(model);
		}

		[HttpPost]
		public ActionResult PageDemo(int Page)
		{
			var model = new ViewModel
			{
				CustomerItem = dataservice.Fun_GCustomer(Page, 10)
			};

			return PartialView("_PartialView", model);
		}
	}

	public class Service
	{
		private NorthwindNETEntities db = new NorthwindNETEntities();

		public IPageList<Customer> Fun_GCustomer(int NowPage, int PageSize)
		{
			var CustomerData = db.Customer.OrderBy(x => x.ID).SkipTakeList(NowPage, PageSize);

			return CustomerData;
		}
	}

	public class ViewModel
	{
		public IPageList<Customer> CustomerItem { get; set; }
	}
}