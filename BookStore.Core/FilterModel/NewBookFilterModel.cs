
using Microsoft.AspNetCore.Http;

namespace BookStore.Core.FilterModel
{
	public class NewBookFilterModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public string Image { get; set; }
		public int IdCategory { get; set; }
		public int IdAuthor { get; set; }
		public IFormFile file { get; set; }
	}
}
