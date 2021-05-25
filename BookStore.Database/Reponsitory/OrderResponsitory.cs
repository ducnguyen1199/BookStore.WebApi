using BookStore.Core.Entity;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Database.Reponsitory
{
	public class OrderResponsitory : IOrderReponsitory
	{
		private readonly ApplicationDbContext _context;
		public OrderResponsitory(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task Commit() => await _context.SaveChangesAsync();
		public async Task AddOrder(Order order) => await _context.Orders.AddAsync(order);
		public async Task<ListItemResponse<Order>> GetListOrder()
		{
			IQueryable<Order> orders = _context.Orders;
			return new ListItemResponse<Order>()
			{
				Data = await orders.ToListAsync(),
				Count = await orders.CountAsync()
			};
		}
		public async Task AddBooksIntoOrder(List<DetailOrder> detailOrders)
		{
			await _context.DetailOrders.AddRangeAsync(detailOrders);
		}
		public async Task Delete(int id)
		{
			Order order = await _context.Orders.FindAsync(id);
			_context.Orders.Remove(order);
		}

		public async Task<Order> GetById(int id){
			return await _context.Orders.Include(o => o.DetailOrders).FirstOrDefaultAsync(o=>o.Id == id);
	}
	}
}
