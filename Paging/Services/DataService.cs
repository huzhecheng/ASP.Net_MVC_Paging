using Paging.Interface;
using Paging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Paging.Extention;
using Paging.Tool;

namespace Paging.Services
{
	public class DataService
	{
		private NorthwindNETEntities db = new NorthwindNETEntities();

		public IPageList<Customer> GetCustomer(int Page)
		{
			var data = db.Customer.OrderBy(x => x.ID).SkipTakeList(Page, 10);

			return data;
		}

		public Tuple<object, PageInfo> GetData(int Page)
		{
			var data = db.Customer.Select(x => new
			{
				x.ID,
				x.Name,
				x.City,
				x.Country,
				x.Phone
			}).OrderBy(x => x.ID).SkipTakeList(Page, 10);

			return new Tuple<object, PageInfo>(data, data.pageinfo);
		}
	}
}