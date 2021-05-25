using Microsoft.AspNetCore.Http;
using System;
namespace BookStore.Core.UpdateModel
{
	public class BookUpdateModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public string Image { get; set; }
		public IFormFile file { get; set; }
		public DateTime DateCreated { get; set; }
		public int IdCategory { get; set; }
		public int IdAuthor { get; set; }
	}
}
