using Paging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paging.Helper
{
	public static class PageControl
	{
		public static MvcHtmlString PageHelper<T>(this HtmlHelper helper, IPageList<T> list)
		{
			if (list == null || list.Count() <= 0)
			{
				return null;
			}

			TagBuilder ul = new TagBuilder("ul");
			ul.MergeAttribute("class", "pagination");

			//上一頁    
			if (list.pageinfo.NowPage != 1)
			{
				TagBuilder liFirst = new TagBuilder("li");
				TagBuilder aFirst = new TagBuilder("a");
				TagBuilder spanFirst = new TagBuilder("span");
				spanFirst.AddCssClass("fa fa-angle-double-left");

				if (list.pageinfo.NowPage == 2)
				{
					aFirst.AddCssClass("noactive");
				}

				aFirst.InnerHtml = spanFirst.ToString();
				aFirst.MergeAttribute("index", "1");
				liFirst.InnerHtml += aFirst.ToString();
				ul.InnerHtml += liFirst;

				TagBuilder aPrev = new TagBuilder("a");
				TagBuilder liPrev = new TagBuilder("li");
				TagBuilder spanPrev = new TagBuilder("span");
				spanPrev.AddCssClass("fa fa-angle-left");

				aPrev.InnerHtml = spanPrev.ToString();
				aPrev.AddCssClass("arrow");
				aPrev.MergeAttribute("index", (list.pageinfo.NowPage - 1).ToString());
				liPrev.InnerHtml = aPrev.ToString();
				ul.InnerHtml += liPrev.ToString();
			}

			var max = (list.pageinfo.NowPage + 5) >= list.pageinfo.MaxPage ? list.pageinfo.MaxPage : list.pageinfo.MaxPage + 5;
			var min = (list.pageinfo.NowPage - 5) > 0 ? list.pageinfo.NowPage - 5 : 1;

			for (int i = min; i <= max; i++)
			{
				TagBuilder li = new TagBuilder("li");
				TagBuilder a = new TagBuilder("a");
				a.InnerHtml = i + "";

				if (i == list.pageinfo.NowPage)
				{
					a.AddCssClass("active nowpage");
					a.MergeAttribute("index", i.ToString());
				}

				else
				{
					a.MergeAttribute("index", i.ToString());

					if (i == list.pageinfo.NowPage - 1 || i == list.pageinfo.NowPage + 1)
					{
						a.AddCssClass("active");
					}
					else if (i == list.pageinfo.NowPage - 2 || i == list.pageinfo.NowPage + 2)
					{
						a.AddCssClass("active");
					}
					else
					{
						a.AddCssClass("noactive");
					}
				}
				li.InnerHtml = a.ToString();
				ul.InnerHtml += li;
			}

			//下一頁 
			if (list.pageinfo.NowPage != list.pageinfo.MaxPage)
			{
				TagBuilder aNext = new TagBuilder("a");
				TagBuilder liNext = new TagBuilder("li");
				TagBuilder spanNext = new TagBuilder("span");
				spanNext.AddCssClass("fa fa-angle-right");

				aNext.InnerHtml = spanNext.ToString();
				aNext.AddCssClass("arrow");
				aNext.MergeAttribute("index", (list.pageinfo.NowPage + 1).ToString());
				liNext.InnerHtml += aNext.ToString();
				ul.InnerHtml += liNext;

				TagBuilder aLast = new TagBuilder("a");
				TagBuilder liLast = new TagBuilder("li");
				TagBuilder spanLast = new TagBuilder("span");
				spanLast.AddCssClass("fa fa-angle-double-right");

				if (list.pageinfo.NowPage == list.pageinfo.MaxPage - 1)
				{
					aLast.AddCssClass("noactive");
				}

				aLast.InnerHtml = spanLast.ToString();
				aLast.MergeAttribute("index", list.pageinfo.MaxPage.ToString());
				liLast.InnerHtml += aLast.ToString();
				ul.InnerHtml += liLast;
			}

			return MvcHtmlString.Create(ul.ToString());
		}
	}
}