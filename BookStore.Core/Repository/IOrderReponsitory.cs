using BookStore.Core.Entity;
using BookStore.Core.Shared;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
	public interface IOrderReponsitory: IRepository
	{
		Task AddOrder(Order order);
		Task<ListItemResponse<Order>> GetListOrder();
	}
}
