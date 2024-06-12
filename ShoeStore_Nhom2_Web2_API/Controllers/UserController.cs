using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeStore_Nhom2_Web2_API.Models;
using ShoeStore_Nhom2_Web2_API.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeStore_Nhom2_Web2_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;

		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		//POST:/api/Auth/Register - chức năng đăng ký user 
		[HttpPost("Register")]
		public async Task<IActionResult> Register(RegisterRequestDTO registerRequestDTO)
		{
			var result = await _userRepository.RegisterUserAsync(registerRequestDTO);
			if (result.Succeeded)
			{
				return Ok("Register Successful! Let login!");
			}
			return BadRequest(result.Errors.FirstOrDefault()?.Description ?? "Something went wrong!");
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
		{
			var (IsSuccess, LoginResponse, Error) = await _userRepository.LoginUserAsync(loginRequestDTO);
			if (IsSuccess)
			{
				return Ok(LoginResponse);
			}
			return BadRequest(Error);
		}
	}
}
