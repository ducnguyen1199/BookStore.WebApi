using AutoMapper;
using BookStore.Core.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStore.Application.IService;
using BookStore.Core.FilterModel;
using System.Collections.Generic;
using BookStore.Core.UpdateModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using BookStore.Core.ViewModel;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BookStore.Core.Enum;

namespace BookStore.Controllers
{
	[Authorize]
	[Route("api/[Controller]")]
	[ApiController]
	public class UserController: ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IUserReponsitory _userReponsitory;
		private readonly IHostingEnvironment _environment;
		private readonly IMapper _mapper;
		public UserController(IUserReponsitory userReponsitory, IMapper mapper, IUserService userService, IHostingEnvironment environment)
		{
			_userReponsitory = userReponsitory;
			_mapper = mapper;
			_userService = userService;
			_environment = environment;
		}
		[Authorize(Roles = RoleType.Admin)]
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _userService.Get());
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpGet("{idUser}")]
		public async Task<IActionResult> GetById(int idUser)
		{
			var user = await _userService.GetById(idUser);
			if (user == null) return BadRequest(new { success = false, message = "User is undefinded" });
			return Ok(user);
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpPut("UpdateInfoUser/{idUser}")]
		public async Task<IActionResult> UpdateInfoUser(int idUser, UserUpdateModel filter)
		{
			var user = await _userService.UpdateInfoUser(idUser, filter);
			return Ok(user);
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpPut("UpdateAvatar/{idUser}")]
		public async Task<IActionResult> UpdateAvatar(int idUser, IFormFile file)
		{
			string urlImg = await UploadImg(file);
			if (urlImg == null) BadRequest(new { success = false, message = "Avatar is empty!" });
			string avatar = urlImg + idUser + file.FileName;
			
			var user = await _userService.UpdateAvatar(idUser, avatar);
			return Ok(user);
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpGet("Like/{idUser}")]
		public async Task<IActionResult> GetBookLiked(int idUser)
		{
			var bookTest = await _userService.GetBookLiked(idUser);
			return Ok(bookTest);
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpPost("like")]
		public async Task<IActionResult> Like(LikeFilterModel filter)
		{
			return Ok(await _userService.Like(filter.IdUser, filter.IdBook));
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpDelete("UnLike/{idLiked}")]
		public async Task<IActionResult> UnLike(int idLiked)
		{
			await _userService.UnLike(idLiked);
			return Ok(new { success = true, meessage = "UnLiked" });
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpGet("BooksInCart/{idUser}")]
		public async Task<IActionResult> GetBooksInCart(int idUser)
		{
			var books = await _userService.GetBookInCart(idUser);
			return Ok(new
			{
				data = books,
				subtotal = books.Sum(b => b.SubTotal)
			});
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpPost("AddBookIntoCart")]
		public async Task<IActionResult> AddBookIntoCart(BookInCartFilterModel filter)
		{
			if (filter.Quantity < 1) return BadRequest(new { Success = false, Message = "Quantity >= 1" });

			return Ok(await _userService.AddBookIntoCart(filter));
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpDelete("DeleteBookFromCart/{id}")]
		public async Task<IActionResult> DeleteBookIntoCart(int id)
		{
			await _userService.DeleteBookFromCart(new List<int>() { id });
			return Ok(new { success = false, message = "Book was deleted!"});
		}
		[Authorize(Roles = RoleType.AdminOrCustomer)]
		[HttpPut("UpdateBookInCart/{id}")]
		public async Task<IActionResult> UpdateBookInCart(int id, int quantity)
		{
			return Ok(await _userService.UpdateBookInCart(id, quantity));
		}
		private async Task<string> UploadImg(IFormFile file)
		{
			var uploads = Path.Combine(_environment.WebRootPath, "avatars");
			if (file.Length > 0)
			{
				using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}
				return "https://localhost:44369/avatar/";
			}
			return null;
		}
	}
}
