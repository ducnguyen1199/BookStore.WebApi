using AutoMapper;
using BookStore.Core.Entity;
using System.Collections.Generic;

namespace BookStore.Core.ViewModel
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Book, BookViewModel>();
			CreateMap<Author, AuthorViewModel>();
			CreateMap<BookViewModel, Book>();
			CreateMap<User, UserViewModel>();
			CreateMap<FavoriteBook, FavoriteViewModel>();
			CreateMap<BooksInCart, BooksInCartViewModel>();
			CreateMap<Order, OrderViewModel>();
			CreateMap<DetailOrder, DetailOrderViewModel > ();
		}
	}
}
