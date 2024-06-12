using Microsoft.AspNetCore.Mvc;
using ShoeStore_Nhom2_Web2.Models;
using System.Diagnostics;
using System.Net.Http;

namespace ShoeStore_Nhom2_Web2.Controllers
{
	public class HomeController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpContextAccessor _contextAccessor;
		public HomeController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7213");
			_contextAccessor = httpContextAccessor;
		}
		public async Task<IActionResult> Index(string? brand = null, string? color = null)
		{
			var response = await _httpClient.GetAsync($"api/Shoe/get-shoe-F?brand={brand}&color={color}");
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


	}
}
