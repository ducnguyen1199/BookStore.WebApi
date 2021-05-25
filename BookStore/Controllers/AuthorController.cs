using AutoMapper;
using BookStore.Application.IService;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthorController: ControllerBase
	{
		private readonly IAuthorReponsitory _authorReponsitory;
		private readonly IAuthorService _authorService;
		private readonly IMapper _mapper; 
		private readonly IHostingEnvironment _environment;
		public AuthorController(IAuthorReponsitory authorReponsitory, IMapper mapper, IHostingEnvironment environment, IAuthorService authorService)
		{
			_authorReponsitory = authorReponsitory;
			_mapper = mapper;
			_environment = environment;
			_authorService = authorService;
		}
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _authorService.Get());
		}
		[HttpPost]
		public async Task<IActionResult> Add([FromForm]AuthorFilterModel filter)
		{
			filter.Avatar = await this.UploadImg(filter.file);
			return Ok(await _authorService.Add(filter));
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await _authorService.Delete(id);
			return Ok();
		}
		[HttpPut("{idAuthor}")]
		public async Task<IActionResult> Update(int idAuthor, [FromForm] AuthorFilterModel filter)
		{
			filter.Avatar = await this.UploadImg(filter.file);
			await _authorService.Update(idAuthor, filter);
			return Ok();
		}

		private async Task<string> UploadImg(IFormFile file)
		{
			var uploads = Path.Combine(_environment.WebRootPath, "authorImgs");
			if (file.Length > 0)
			{
				using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}
				return "https://localhost:44369/authorImgs/" + file.FileName;
			}
			return "";
		}
	}
}
