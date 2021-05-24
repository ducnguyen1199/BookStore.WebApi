using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using BookStore.Core.UpdateModel;
using BookStore.Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Database.Reponsitory
{
	public class BookReponsitory: IBookReponsitory
	{
		private readonly ApplicationDbContext _context;

		public BookReponsitory(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task Commit() => await _context.SaveChangesAsync();
		public async Task Add(Book book) => await _context.Books.AddAsync(book);
		public async Task Delete(int id)=> _context.Books.Remove(await _context.Books.FindAsync(id));
		public async Task<ListItemResponse<Book>> Get(BookFilterModel filter)
		{
			IQueryable<Book> books = _context.Books.Include(b=>b.Category).Include(b=>b.Author);

			if (!string.IsNullOrWhiteSpace(filter.KeyWord))
			{
				filter.KeyWord = filter.KeyWord.Trim();;
				books = books.Where(b => b.Name.ToLower().Contains(filter.KeyWord));
			}

			if (filter.IdCategory != null)
			{
				books = books.Where(b => b.IdCategory == filter.IdCategory);
			}

			if (filter.IdAuthor != null)
			{
				books = books.Where(b => b.IdAuthor == filter.IdAuthor);
			}

			if (filter.OrderBy != null)
			{
				switch (filter.OrderBy)
				{
					case TypeOrderBy.PriceDESC:
						books.OrderByDescending(b => b.Price);
						break;
					case TypeOrderBy.PriceASC:
						books.OrderBy(b => b.Price);
						break;
					case TypeOrderBy.DateCreated:
						books.OrderByDescending(b => b.DateCreated);
						break;
					default:
						break;
				}
			}
			
			if(filter.Skip == -1 && filter.Offset == -1)
			{
				return new ListItemResponse<Book>
				{
					Data = await books.ToListAsync(),
					Count = await books.CountAsync()
				};
			}
			
			return new ListItemResponse<Book>
			{
				Data = await books.Skip((filter.Skip - 1) * filter.Offset).Take(filter.Offset).ToListAsync(),
				Count = await books.CountAsync()
			};
		}
		public async Task<Book> GetDetail(int id) => await _context.Books.Include(b => b.Category).Include(b => b.Author).FirstOrDefaultAsync(b=>b.Id==id);
		public async Task Update( BookUpdateModel data)
		{
			Book book = await _context.Books.FindAsync(data.Id);
			book.IdAuthor = data.IdAuthor;
			book.IdCategory = data.IdCategory;
			book.Description = data.Description;
			book.Price = data.Price;
			book.Name = data.Name;
			book.Image = data.Image;
		}
	}
}
