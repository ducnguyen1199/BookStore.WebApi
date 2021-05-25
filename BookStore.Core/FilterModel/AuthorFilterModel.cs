using Microsoft.AspNetCore.Http;
using System;
namespace BookStore.Core.FilterModel
{
	public class AuthorFilterModel
	{
		public string Name { get; set; }
		public DateTime BirthDay { get; set; }
		public string Story { get; set; }
		public string Avatar { get; set; }
		public  IFormFile file {get;set;}
	}
}
