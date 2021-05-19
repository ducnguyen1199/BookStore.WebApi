using AutoMapper;
using BookStore.Core.Entity;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryReponsitory _categoryReponsitory;
		private readonly IMapper _mapper;
		public CategoryController(ICategoryReponsitory categoryReponsitory, IMapper mapper)
		{
			_categoryReponsitory = categoryReponsitory;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			ListItemResponse<Category> categories = await _categoryReponsitory.Get();
			return Ok(categories);
		}
		[HttpPost]
		public async Task<IActionResult> Add(Category category)
		{
			await _categoryReponsitory.Add(category);
			await _categoryReponsitory.Commit();
			return Ok();
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await _categoryReponsitory.Delete(id);
			await _categoryReponsitory.Commit();
			return Ok();
		}
	}
}
