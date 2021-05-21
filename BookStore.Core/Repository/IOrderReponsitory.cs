using BookStore.Core.Entity;
using BookStore.Core.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
	public interface IOrderReponsitory: IRepository
	{
		Task AddOrder(Order order);
		Task<ListItemResponse<Order>> GetListOrder();
		Task AddBooksIntoOrder(List<DetailOrder> detailOrders);
		Task Delete(int id);
	}
}
