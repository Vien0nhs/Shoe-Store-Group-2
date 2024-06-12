using Microsoft.AspNetCore.Mvc;
using ShoeStore_Nhom2_Web2.Models;
using Newtonsoft.Json;
using ShoeStore_Nhom2_Web2.Repositories;
#nullable disable
namespace ShoeStore_Nhom2_Web2.Controllers
{
	public class ShopController : Controller
	{
		private readonly IShopRepository _shopRepository;
		private readonly IHttpContextAccessor _contextAccessor;

		public ShopController(IShopRepository shopRepository, IHttpContextAccessor httpContextAccessor)
		{
			_shopRepository = shopRepository;
			_contextAccessor = httpContextAccessor;
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RegisterP(RegisterRequestDTO requestDTO)
		{
			var response = await _shopRepository.RegisterUserAsync(requestDTO);
			if (response.IsSuccessStatusCode)
			{
				ViewBag.Ok = "Thông báo: Đăng ký thành công, đăng nhập ngay!";
				return View("Register");
			}
			else
			{
				ViewBag.Fail = "Lỗi: Xác nhận mật khẩu không chính xác, vui lòng thử lại";
				return View("Register");
			}
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Loginp(LoginViewModel loginModel)
		{
			var CheckLoginn = loginModel.Username;
			var response = await _shopRepository.LoginUserAsync(loginModel);
			if (response.IsSuccessStatusCode)
			{
				_contextAccessor.HttpContext.Session.SetString("CheckLoginn", CheckLoginn); // Session

				var token = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
				var tokenn = token.JwtToken;
				var Roles = token.Roles.ToList();
				_contextAccessor.HttpContext.Session.SetString("JWTToken", tokenn); // Session
				var RoleJson = JsonConvert.SerializeObject(Roles);
				HttpContext.Session.SetString("Role", RoleJson); // Session
				ViewData["Token"] = tokenn;

				if (Roles.Contains("Write"))
				{
					var CheckLogin = "Vien";
					_contextAccessor.HttpContext.Session.SetString("CheckLogin", CheckLogin); // Session
					return RedirectToAction("Index", "Home");
				}
				else if (Roles.Contains("Read"))
				{
					return RedirectToAction("Index", "Home");
				}
				else if (!string.IsNullOrEmpty(tokenn))
				{
					return RedirectToAction("Index", "Home");
				}
			}
			else
			{
				ViewBag.InvalidCredentials = "Invalid username or password.";
				return View("Login");
			}

			return BadRequest();
		}

		public IActionResult Logout()
		{
			_contextAccessor.HttpContext.Session.Remove("JWTToken");
			_contextAccessor.HttpContext.Session.Remove("Role");
			_contextAccessor.HttpContext.Session.Remove("CheckLogin");
			_contextAccessor.HttpContext.Session.Remove("CheckLoginn");

			return RedirectToAction("Login");
		}

		public async Task<IActionResult> Category(string brand = null, string color = null)
		{
			var response = await _shopRepository.GetFilteredShoesAsync(brand, color);
			if (response.IsSuccessStatusCode)
			{
				var shoes = await response.Content.ReadFromJsonAsync<List<Shoe_Model>>();
				return View(shoes);
			}
			else
			{
				return StatusCode((int)response.StatusCode);
			}
		}

		public async Task<IActionResult> CategoryP(int page = 1, int pageSize = 10)
		{
			ViewData["page"] = page;
			ViewData["size"] = pageSize;
			if (page <= 0 || pageSize <= 0) return BadRequest("Số trang và kích thước trang phải từ 1 trở lên");

			var response = await _shopRepository.GetShoesByPageAsync(page, pageSize);
			if (response.IsSuccessStatusCode)
			{
				var shoes = await response.Content.ReadFromJsonAsync<List<Shoe_Model>>();
				return View("Category", shoes);
			}
			else
			{
				return StatusCode((int)response.StatusCode);
			}
		}

		public async Task<IActionResult> Detail(int id)
		{
			var response = await _shopRepository.GetShoeDetailAsync(id);
			if (response.IsSuccessStatusCode)
			{
				var shoe = await response.Content.ReadFromJsonAsync<Shoe_Model>();
				return View(shoe);
			}
			else
			{
				return StatusCode((int)response.StatusCode);
			}
		}

		public IActionResult Cart()
		{
			var Role = _contextAccessor.HttpContext.Session.GetString("Role");

			if (string.IsNullOrEmpty(Role))
			{
				return StatusCode(StatusCodes.Status401Unauthorized, "Bạn phải đăng nhập để dùng chức năng này. ");
			}
			if (Role.Contains("Read"))
			{
				return View();
			}
			return BadRequest();
		}

		public IActionResult Checkout()
		{
			return View();
		}

		public IActionResult Confirm()
		{
			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}
	}
}
