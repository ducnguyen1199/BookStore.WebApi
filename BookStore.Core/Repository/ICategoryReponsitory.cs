using BookStore.Core.Entity;
using BookStore.Core.Shared;
using System.Threading.Tasks;
using TopCV.Core.Repository;

namespace BookStore.Core.Repository
{
	public interface ICategoryReponsitory : IRepository<Category>
	{
		Task<ListItemResponse<Category>> Get();
		Task Add(Category category);
		Task Delete(int id);
	}
}
