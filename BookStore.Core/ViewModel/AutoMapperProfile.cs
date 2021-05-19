using AutoMapper;
using BookStore.Core.Entity;

namespace BookStore.Core.ViewModel
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Book, BookViewModel>();
			CreateMap<Author, AuthorViewModel>();
			CreateMap<BookViewModel, Book>();
		}
	}
}
