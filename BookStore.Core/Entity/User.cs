using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Core.Entity
{
	public class User : IdentityUser<int>
	{
		[Required]
		public string FullName { get; set; }
		[Required]
		public DateTime BirthDay { get; set; }
		public string Avatar { get; set; }
		public virtual ICollection<IdentityUserRole<int>> UserRoles { get; } = new List<IdentityUserRole<int>>();
		public virtual ICollection<Order> Orders { get; } = new List<Order>();
		public virtual ICollection<FavoriteBook> FavoriteBooks { get; } = new List<FavoriteBook>();
		public virtual ICollection<BooksInCart> BooksInCarts { get; } = new List<BooksInCart>();
	}
}
