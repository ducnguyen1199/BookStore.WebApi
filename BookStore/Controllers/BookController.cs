using AutoMapper;
using BookStore.Application.IService;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using BookStore.Core.UpdateModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookController : ControllerBase
	{
		private readonly IBookReponsitory _bookReponsitory;
		private readonly IMapper _mapper;
		private readonly IBookService _bookService;
		private readonly IHostingEnvironment _environment;
		public BookController(IBookReponsitory bookReponsitory, IMapper mapper, IBookService bookService, IHostingEnvironment environment)
		{
			_bookReponsitory = bookReponsitory;
			_mapper = mapper;
			_bookService = bookService;
			_environment = environment;
		}
		[HttpGet]
		public async Task<IActionResult> Get(string KeyWord, int? IdCategory, int? IdAuthor, int? OrderBy, int? Skip, int? Offset)
		{
			if (Skip == null || Offset == null) {

				Skip = -1;
				Offset = -1;
			}
			return Ok(await _bookService.GetAll(KeyWord, IdCategory, IdAuthor, OrderBy, (int)Skip, (int)Offset));
		}
		[HttpGet("{idBook}")]
		public async Task<IActionResult> GetDetail(int idBook)
		{
			return Ok(await _bookService.GetDetail(idBook));
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromForm]NewBookFilterModel filter)
		{
			filter.Image = await UploadImg(filter.file);
			return Ok(await _bookService.Add(filter));
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await _bookService.Delete(id);
			return Ok();
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromForm] BookUpdateModel filter)
		{
			filter.Image = await UploadImg(filter.file);
			await _bookService.Update(id,filter);
			return Ok();
		}

		private async Task<string> UploadImg(IFormFile file)
		{

			var uploads = Path.Combine(_environment.WebRootPath, "bookImgs");
			if (file.Length > 0)
			{
				using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}
				return "https://localhost:44369/bookImgs/" + file.FileName;
			}
			return "";
		}
	}
}
