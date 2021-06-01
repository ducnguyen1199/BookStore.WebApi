using AutoMapper;
using BookStore.Application.IService;
using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using BookStore.Core.UpdateModel;
using BookStore.Core.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Service
{
	public class BookService : IBookService
	{
		private readonly IBookReponsitory _bookReponsitory;
		private readonly IMapper _mapper;
		public BookService(IBookReponsitory bookReponsitory, IMapper mapper)
		{
			_bookReponsitory = bookReponsitory;
			_mapper = mapper;
		}

		public async Task<BookViewModel> Add(NewBookFilterModel filter)
		{
			Book book = await _bookReponsitory.Add(_mapper.Map<Book>(filter));
			await _bookReponsitory.Commit();
			BookViewModel newBook = _mapper.Map<BookViewModel>(await _bookReponsitory.GetDetail(book.Id));
			return newBook;
		}

		public async Task Delete(int id)
		{
			await _bookReponsitory.Delete(id);
			await _bookReponsitory.Commit();
		}

		public async Task<ListItemResponse<BookViewModel>> GetAll(BookFilterModel filter)
		{
			ListItemResponse<Book> books = await _bookReponsitory.Get(filter);
			return new ListItemResponse<BookViewModel>
			{
				Data = books.Data.Select(u => _mapper.Map<BookViewModel>(u)),
				Count = books.Count
			};
		}
		public async Task<BookViewModel> GetDetail(int id)
		{
			Book book = await _bookReponsitory.GetDetail(id);
			return  _mapper.Map<BookViewModel>(book);
		}

		public async Task<ListItemResponse<BookViewModel>> GetTrending()
		{
			ListItemResponse<Book> books = await _bookReponsitory.GetTrending();
			return new ListItemResponse<BookViewModel>
			{
				Data = books.Data.Select(u => _mapper.Map<BookViewModel>(u)),
				Count = books.Count
			};
		}

		public async Task Update(int id, BookUpdateModel data)
		{
			await _bookReponsitory.Update(id, data);
			await _bookReponsitory.Commit();
		}
	}
}
