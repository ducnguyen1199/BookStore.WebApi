using AutoMapper;
using BookStore.Core.Entity;
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
	public class AuthorController: ControllerBase
	{
		private readonly IAuthorReponsitory _authorReponsitory;
		private readonly IMapper _mapper;
		public AuthorController(IAuthorReponsitory authorReponsitory, IMapper mapper)
		{
			_authorReponsitory = authorReponsitory;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			
			ListItemResponse<Author> books = await _authorReponsitory.Get();

			return Ok(new ListItemResponse<AuthorViewModel>
			{
				Data = books.Data.Select(u => _mapper.Map<AuthorViewModel>(u)),
				Count = books.Count
			});
		}
		[HttpPost]
		public async Task<IActionResult> Add(Author author)
		{
			await _authorReponsitory.Add(author);
			await _authorReponsitory.Commit();
			return Ok();
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await _authorReponsitory.Delete(id);
			await _authorReponsitory.Commit();
			return Ok();
		}
	}
}
