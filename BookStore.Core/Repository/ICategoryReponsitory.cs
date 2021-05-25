using BookStore.Core.Entity;
using BookStore.Core.Shared;
using System.Threading.Tasks;
namespace BookStore.Core.Repository
{
	public interface ICategoryReponsitory : IRepository 
	{
		Task<ListItemResponse<Category>> Get();
		Task<Category> Add(Category category);
		Task Delete(int id);
	}
}
