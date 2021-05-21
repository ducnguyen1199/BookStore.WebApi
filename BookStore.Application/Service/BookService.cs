using AutoMapper;
using BookStore.Application.IService;
using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
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
		public async Task<ListItemResponse<BookViewModel>> GetAll(string KeyWord, int? IdCategory, int? IdAuthor, int? OrderBy, int Skip, int Offset)
		{
			BookFilterModel filter = new BookFilterModel
			{
				KeyWord = KeyWord,
				IdCategory = IdCategory,
				IdAuthor = IdAuthor,
				OrderBy = (TypeOrderBy?)OrderBy,
				Skip = Skip,
				Offset = Offset
			};
			ListItemResponse<Book> books = await _bookReponsitory.Get(filter);
			return new ListItemResponse<BookViewModel>
			{
				Data = books.Data.Select(u => _mapper.Map<BookViewModel>(u)),
				Count = books.Count
			};
		}
	}
}
