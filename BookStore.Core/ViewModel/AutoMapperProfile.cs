using AutoMapper;
using BookStore.Core.Entity;
using BookStore.Core.FilterModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Core.ViewModel
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Book, BookViewModel>();
			CreateMap<Author, AuthorViewModel>().ForMember(vm => vm.BookCount, options => options.MapFrom(a => a.Books.Count));
			CreateMap<AuthorFilterModel, Author>();
			CreateMap<BookViewModel, Book>();
			CreateMap<NewBookFilterModel, Book>();
			CreateMap<User, UserViewModel>()
				.ForMember(vm => vm.RoleIds, options => options.MapFrom(u => u.UserRoles.Select(ur => ur.RoleId)));

			CreateMap<FavoriteBook, FavoriteViewModel>();
			CreateMap<BooksInCart, BooksInCartViewModel>();
			CreateMap<Order, OrderViewModel>();
			CreateMap<DetailOrder, DetailOrderViewModel > ();
		}
	}
}
