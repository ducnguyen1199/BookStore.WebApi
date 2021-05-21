using AutoMapper;
using BookStore.Application.IService;
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
		private readonly IBookService _bookService;
		public BookController(IBookReponsitory bookReponsitory, IMapper mapper, IBookService bookService)
		{
			_bookReponsitory = bookReponsitory;
			_mapper = mapper;
			_bookService = bookService;
		}
		[HttpGet]
		public async Task<IActionResult> Get(string KeyWord, int? IdCategory, int? IdAuthor, int? OrderBy, int Skip, int Offset)
		{
			return Ok(await _bookService.GetAll(KeyWord, IdCategory, IdAuthor, OrderBy, Skip, Offset));
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
