using AutoMapper;
using BookStore.Core.Entity;
using BookStore.Core.FilterModel;

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
			CreateMap<User, UserViewModel>();
			CreateMap<FavoriteBook, FavoriteViewModel>();
			CreateMap<BooksInCart, BooksInCartViewModel>();
			CreateMap<Order, OrderViewModel>();
			CreateMap<DetailOrder, DetailOrderViewModel > ();
		}
	}
}
