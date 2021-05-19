using BookStore.Core.Entity;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Database.Reponsitory
{
	public class AuthorReponsitory : IAuthorReponsitory
	{
		private readonly ApplicationDbContext _context;
		public AuthorReponsitory(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Commit() => await _context.SaveChangesAsync();

		public async Task Add(Author author) => await _context.Authors.AddAsync(author);

		public async Task Delete(int id) => _context.Authors.Remove(await _context.Authors.FindAsync(id));		

		public async Task<ListItemResponse<Author>> Get()
		{
			IQueryable<Author> authors = _context.Authors;
			return new ListItemResponse<Author>
			{
				Data = await authors.ToListAsync(),
				Count = await authors.CountAsync()
			};
		}
	}
}
