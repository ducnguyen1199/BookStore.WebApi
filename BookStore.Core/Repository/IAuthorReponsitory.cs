using BookStore.Core.Entity;
using BookStore.Core.Shared;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
	public interface IAuthorReponsitory: IRepository
	{
		Task<ListItemResponse<Author>> Get();
		Task Add(Author autor);
		Task Delete(int id);
	}
}
