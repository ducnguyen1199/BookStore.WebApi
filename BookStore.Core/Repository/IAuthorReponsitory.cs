using BookStore.Core.Entity;
using BookStore.Core.Shared;
using System.Threading.Tasks;
using TopCV.Core.Repository;

namespace BookStore.Core.Repository
{
	public interface IAuthorReponsitory: IRepository<Author>
	{
		Task<ListItemResponse<Author>> Get();
		Task Add(Author autor);
		Task Delete(int id);
	}
}
