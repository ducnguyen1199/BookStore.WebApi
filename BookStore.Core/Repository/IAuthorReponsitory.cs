using BookStore.Core.Entity;
using BookStore.Core.Shared;
using BookStore.Core.ViewModel;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
	public interface IAuthorReponsitory: IRepository
	{
		Task<ListItemResponse<Author>> Get();
		Task<Author> Get(int id);
		Task<Author> Add(Author author);
		Task Delete(int id);
		Task Update(int Id, Author author);
	}
}
