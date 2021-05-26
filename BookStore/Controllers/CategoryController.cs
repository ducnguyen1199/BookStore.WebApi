using AutoMapper;
using BookStore.Core.Entity;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
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
		public async Task<IActionResult> Add(string Name)
		{
			if (string.IsNullOrWhiteSpace(Name)) return BadRequest(new { Success = false, Message = "Category is required" });
			Category category = await _categoryReponsitory.Add(new Category() { Name = Name});
			await _categoryReponsitory.Commit();
			return Ok(category);
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			await _categoryReponsitory.Delete(id);
			await _categoryReponsitory.Commit();
			return Ok(new { success = true, message = "Category deleted!"});
		}
	}
}
