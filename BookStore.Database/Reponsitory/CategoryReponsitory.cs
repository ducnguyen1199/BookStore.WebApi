using BookStore.Core.Entity;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Database.Reponsitory
{
	public class CategoryReponsitory : ICategoryReponsitory
	{
		private readonly ApplicationDbContext _context;
		public CategoryReponsitory(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Commit() => await _context.SaveChangesAsync();

		public async Task<Category> Add(Category category) {
			await _context.Categories.AddAsync(category);
			return category;
		} 

		public async Task Delete(int id) => _context.Categories.Remove(await _context.Categories.FindAsync(id));

		public async Task<ListItemResponse<Category>> Get()
		{
			IQueryable<Category> categories = _context.Categories;
			return new ListItemResponse<Category>
			{
				Data = await categories.ToListAsync(),
				Count = await categories.CountAsync()
			};
		}
	}
}
