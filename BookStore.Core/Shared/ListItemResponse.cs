using System.Collections.Generic;

namespace BookStore.Core.Shared
{
	public class ListItemResponse<T>
	{
		public IEnumerable<T> Data { get; set; }
		public int Count { get; set; }
	}
}
