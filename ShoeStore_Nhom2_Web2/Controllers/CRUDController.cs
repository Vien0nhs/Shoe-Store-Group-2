using Microsoft.AspNetCore.Mvc;
using ShoeStore_Nhom2_Web2.Models;
using ShoeStore_Nhom2_Web2.Repositories;
#nullable disable
namespace ShoeStore_Nhom2_Web2.Controllers
{
	public class CRUDController : Controller
	{
		private readonly IAdminShoeRepository _adminShoeRepository;
		private readonly IHttpContextAccessor _contextAccessor;

		public CRUDController(IAdminShoeRepository adminShoeRepository, IHttpContextAccessor contextAccessor)
		{
			_adminShoeRepository = adminShoeRepository;
			_contextAccessor = contextAccessor;
		}

		// GET: CRUDController
		public async Task<IActionResult> Index()
		{
			var Role = _contextAccessor.HttpContext.Session.GetString("Role");

			if (string.IsNullOrEmpty(Role))
			{
				return StatusCode(StatusCodes.Status401Unauthorized, $"Bạn không có quyền truy cập vào tài nguyên này, vui lòng đăng nhập bằng Admin Account và thử lại.");
			}

			if (Role.Contains("Read") && !Role.Contains("Write"))
			{
				return StatusCode(StatusCodes.Status401Unauthorized, "Hệ thống phát hiện bạn không phải Admin, Bạn không có quyền truy cập vào tài nguyên này, vui lòng thử lại.");
			}

			var response = await _adminShoeRepository.GetAllShoesAsync();
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

		// GET: CRUDController/Details/5
		public async Task<IActionResult> Detail(int id)
		{
			var response = await _adminShoeRepository.GetShoeByIdAsync(id);
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

		// GET: CRUDController/Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ShoeCreateDto shoeCreateDto)
		{
			var response = await _adminShoeRepository.CreateShoeAsync(shoeCreateDto);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return StatusCode((int)response.StatusCode);
			}
		}

		// GET: CRUDController/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			var response = await _adminShoeRepository.GetShoeByIdAsync(id);
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Shoe_Model shoeModel)
		{
			var response = await _adminShoeRepository.UpdateShoeAsync(id, shoeModel);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return StatusCode((int)response.StatusCode);
			}
		}

		// GET: CRUDController/Delete/5
		public async Task<IActionResult> Delete(int id)
		{
			var response = await _adminShoeRepository.GetShoeByIdAsync(id);
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var response = await _adminShoeRepository.DeleteShoeAsync(id);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return StatusCode((int)response.StatusCode);
			}
		}
	}
}
