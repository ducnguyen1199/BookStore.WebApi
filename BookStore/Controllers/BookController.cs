using AutoMapper;
using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using BookStore.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BookController : ControllerBase
	{
		private readonly IBookReponsitory _bookReponsitory;
		private readonly IMapper _mapper;
		public BookController(IBookReponsitory bookReponsitory, IMapper mapper)
		{
			_bookReponsitory = bookReponsitory;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> Get(string KeyWord, int? IdCategory, int? IdAuthor, int? OrderBy, int Skip, int Offset)
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

			return Ok(new ListItemResponse<BookViewModel>
			{
				Data = books.Data.Select(u => _mapper.Map<BookViewModel>(u)),
				Count = books.Count
			});
		}
	
	
		[HttpPost]
		public async Task<IActionResult> Add(BookViewModel bookViewModel)
		{
			await _bookReponsitory.Add(_mapper.Map<Book>(bookViewModel));
			await _bookReponsitory.Commit();
			return Ok();
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await _bookReponsitory.Delete(id);
			await _bookReponsitory.Commit();
			return Ok();
		}
	}
}
