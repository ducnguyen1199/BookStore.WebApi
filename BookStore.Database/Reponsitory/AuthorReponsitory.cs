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
		public async Task<Author> Add(Author author)
		{
			await _context.Authors.AddAsync(author);
			return author;
		}
		public async Task Delete(int id) => _context.Authors.Remove(await _context.Authors.FindAsync(id));
		public async Task<ListItemResponse<Author>> Get()
		{
			IQueryable<Author> authors = _context.Authors.Include(a => a.Books);
			return new ListItemResponse<Author>
			{
				Data = await authors.ToListAsync(),
				Count = await authors.CountAsync()
			};
		}
		public async Task<Author> Get(int id) => await _context.Authors.FindAsync(id);
		public async Task Update(int Id, Author newAuthor)
		{
			Author author = await _context.Authors.FindAsync(Id);
			author.Name = newAuthor.Name;
			author.Story = newAuthor.Story;
			author.BirthDay = newAuthor.BirthDay;
		}
	}
}
