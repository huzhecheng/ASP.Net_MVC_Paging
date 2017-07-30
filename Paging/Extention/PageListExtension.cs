using Paging.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paging.Extention
{
	public static class PageListExtension
	{
		/// <summary>
		/// 擴充方法
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="datalist"></param>
		/// <param name="NowPage"></param>
		/// <param name="PageSize"></param>
		/// <returns></returns>
		public static PageList<T> SkipTakeList<T>(this IQueryable<T> datalist,int NowPage,int PageSize)
		{
			var Itemlist = new PageList<T>(NowPage, PageSize, datalist);

			return Itemlist;
		}
	}
}