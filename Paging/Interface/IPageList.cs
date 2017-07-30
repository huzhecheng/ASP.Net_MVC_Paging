using Paging.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging.Interface
{
	public interface IPageList<T>:IQueryable<T>
	{
		PageInfo pageinfo { get; set; }

		IQueryable<T> DataList { get; set; }

		T t { get; set; }
	}
}
