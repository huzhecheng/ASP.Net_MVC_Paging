using Paging.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Linq.Expressions;

namespace Paging.Tool
{
	public class PageInfo
	{
		/// <summary>
		/// 總筆數
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// 目前頁面
		/// </summary>
		public int NowPage { get; set; }

		/// <summary>
		/// 最大頁數
		/// </summary>
		public int MaxPage { get; set; }

		/// <summary>
		/// 顯示筆數
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// 略過筆數
		/// </summary>
		public int SkipCount { get; set; }

		public bool OverRange { get; set; }

		public PageInfo(int totalCount, int nowPage, int pageSize)
		{
			this.TotalCount = totalCount;

			this.PageSize = pageSize < 1 ? 1 : pageSize;

			SetMaxPageAndNowPage(nowPage, this.TotalCount / this.PageSize);

			this.SkipCount = (this.NowPage - 1) * this.PageSize;
		}


		public void SetMaxPageAndNowPage(int nowPage, int totalPage)
		{
			this.MaxPage = this.TotalCount % this.PageSize == 0 ? totalPage : totalPage + 1;
			if (this.MaxPage >= nowPage)
			{
				this.NowPage = nowPage < 1 ? 1 : nowPage;
				OverRange = false;
			}
			else
			{
				if (this.MaxPage <= 0)
				{
					this.MaxPage = 1;
				}
				this.NowPage = MaxPage;
				OverRange = true;
			}
		}
	}

	public class PageList<T> : IPageList<T>
	{
		private IQueryable<T> _dataList { get; set; }

		public PageInfo pageinfo { get; set; }

		public IQueryable<T> DataList
		{
			get
			{
				return _dataList;
			}
			set
			{
				_dataList = value;
			}
		}

		public T t { get; set; }

		public Expression Expression
		{
			get
			{
				return DataList.Expression;
			}
		}

		public Type ElementType
		{
			get
			{
				return DataList.ElementType;
			}
		}

		public IQueryProvider Provider
		{
			get
			{
				return DataList.Provider;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return DataList.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return DataList.GetEnumerator();
		}

		public PageList(int nowPage, int pageSize, IQueryable<T> DataList)
		{
			bool hasdata = true;

			try
			{
				pageinfo = new PageInfo(DataList.Count(), nowPage, pageSize);

				hasdata = !pageinfo.OverRange;
			}
			catch { hasdata = false; }

			if (hasdata)
			{
				this.DataList = DataList.Skip(pageinfo.SkipCount).Take(pageinfo.PageSize);
			}
			else {
				this.DataList = Enumerable.Empty<T>().AsQueryable();
			}
		}
	}
}