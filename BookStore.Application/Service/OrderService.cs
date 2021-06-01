using AutoMapper;
using BookStore.Application.IService;
using BookStore.Core.Entity;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using BookStore.Core.Shared;
using BookStore.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.Service
{
	public class OrderService: IOrderService
	{
		private readonly IOrderReponsitory _orderReponsitory;
		private readonly IUserReponsitory _userReponsitory;
		private readonly IMapper _mapper;
		public OrderService(IOrderReponsitory orderReponsitory, IUserReponsitory userReponsitory, IMapper mapper)
		{
			_orderReponsitory = orderReponsitory;
			_mapper = mapper;
			_userReponsitory = userReponsitory;
		}
		
		public async Task<Order> AddOrder(OrderFilterModel filter)
		{
			Order order = new Order();
			order.IdUser = filter.IdUser;
			order.Address = filter.Address;
			order.FullName = filter.FullName;
			order.PhoneNumber = filter.PhoneNumber;
			order.Email = filter.Email;
			order.Discount = filter.Discount;
			order.Surcharge = filter.Surcharge;
			await _orderReponsitory.AddOrder(order);
			await _orderReponsitory.Commit();
			return order;
		}
		public async Task<ListItemResponse<OrderViewModel>> GetListOrder()
		{
			ListItemResponse<Order> orders = await _orderReponsitory.GetListOrder();
			return new ListItemResponse<OrderViewModel>()
			{
				Data = orders.Data.Select(o => _mapper.Map<OrderViewModel>(o)),
				Count = orders.Count
			};
		}
		public async Task AddBooksIntoOrder(OrderFilterModel filter)
		{
			Order order = await AddOrder(filter);
			User user = await _userReponsitory.GetDetail(filter.IdUser, DetailType.BooksIntoCart);

			ICollection<BooksInCart> booksInCarts = user.BooksInCarts;
			List<DetailOrder> detailOrders = new List<DetailOrder>();
			List<int> idBookInCarts = new List<int>();
			double subTotal = 0;
			foreach (BooksInCart booksInCart in booksInCarts)
			{
				DetailOrder item = new DetailOrder()
				{
					IdOrder = order.Id,
					IdBook = booksInCart.IdBook,
					Quantity = booksInCart.Quantity,
					Subtotal = booksInCart.SubTotal
				};
				subTotal += booksInCart.SubTotal;
				detailOrders.Add(item);
				idBookInCarts.Add(booksInCart.Id);
			}
			order.Total = subTotal * (1 - order.Surcharge) * (1 + order.Discount);
			await _orderReponsitory.AddBooksIntoOrder(detailOrders);
			await _userReponsitory.DeleteBookFromCart(idBookInCarts);
			await _orderReponsitory.Commit();
		}
		public async Task<ICollection<OrderViewModel>> GetBooksInOrder(int idUser)
		{
			User user = await _userReponsitory.GetDetail(idUser, DetailType.BooksIntoOrder);
			ICollection<Order> orders = user.Orders;
			return _mapper.Map<ICollection<OrderViewModel>>(orders);
		}
		public async Task Delete(int id)
		{
			await _orderReponsitory.Delete(id);
			await _orderReponsitory.Commit();
		}
	}
}
