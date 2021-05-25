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

namespace BookStore.Controllers
{
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
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _userService.Get());
		}
		[HttpGet("{idUser}")]
		public async Task<IActionResult> GetById(int idUser)
		{
			var user = await _userService.GetById(idUser);
			return Ok(user);
		}
		[HttpPut("UpdateInfoUser")]
		public async Task<IActionResult> UpdateInfoUser([FromBody] UserUpdateModel filter)
		{
			var user = await _userService.UpdateInfoUser(filter);
			return Ok(user);
		}
		[HttpPut("UpdateAvatar/{idUser}")]
		public async Task<IActionResult> UpdateAvatar(int idUser, IFormFile file)
		{
			string avatar = await UploadImg(file) + idUser + file.FileName;
			var user = await _userService.UpdateAvatar(idUser, avatar);
			return Ok(user);
		}
		[HttpGet("Like/{idUser}")]
		public async Task<IActionResult> GetBookLiked(int idUser)
		{
			var bookTest = await _userService.GetBookLiked(idUser);
			return Ok(bookTest);
		}
		[HttpPost("like")]
		public async Task<IActionResult> Like(int idUser, int idBook)
		{
			await _userService.Like(idUser, idBook);
			return Ok();
		}
		[HttpDelete("UnLike")]
		public async Task<IActionResult> UnLike(int idUser, int idBook)
		{
			await _userService.UnLike(idUser, idBook);
			return Ok();
		}

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
		[HttpPost("AddBookIntoCart")]
		public async Task<IActionResult> AddBookIntoCart(BookInCartFilterModel fitler)
		{
			return Ok(await _userService.AddBookIntoCart(fitler));
		}
		[HttpDelete("DeleteBookFromCart/{id}")]
		public async Task<IActionResult> DeleteBookIntoCart(int id)
		{
			await _userService.DeleteBookFromCart(new List<int>() { id });
			return Ok();
		}
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
			return "";
		}
	}
}
