using AutoMapper;
using BookStore.Application.IService;
using BookStore.Core.Enum;
using BookStore.Core.FilterModel;
using BookStore.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderReponsitory _orderReponsitory;
		private readonly IMapper _mapper;
		private readonly IOrderService _orderService;
		public OrderController(IOrderReponsitory orderReponsitory, IMapper mapper, IOrderService orderService)
		{
			_orderReponsitory = orderReponsitory;
			_mapper = mapper;
			_orderService = orderService;
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpGet]
		public async Task<IActionResult> GetListOrder()
		{
			return Ok(await _orderService.GetListOrder());
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpGet("{idUser}")]
		public async Task<IActionResult> GetBooksInOrder(int idUser)
		{
			var books = await _orderService.GetBooksInOrder(idUser);
			return Ok(books);
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpPost]
		public async Task<IActionResult> AddBooksIntoOrder([FromBody] OrderFilterModel filter)
		{
			if (filter.Discount > 1 || filter.Discount < 0) return BadRequest(new {success = false, message = "0 <= Discount <= 1" });
			if (filter.Surcharge > 1 || filter.Surcharge < 0) return BadRequest(new { success = false, message = "0 <= Surcharge <= 1" });
			await _orderService.AddBooksIntoOrder(filter);
			return Ok(new { success = true, message = "Books added into order!"});
		}
		[Authorize(Roles = RoleType.Admin)]
		[HttpDelete("{idOrder}")]
		public async Task<IActionResult> Delete(int idOrder)
		{
			await _orderService.Delete(idOrder);
			return Ok(new { success = true, message = "Books was deleted!" });
		}
	}
	
}
