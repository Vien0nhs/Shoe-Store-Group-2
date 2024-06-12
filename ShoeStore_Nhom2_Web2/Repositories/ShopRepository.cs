using ShoeStore_Nhom2_Web2.Models;
using System.Net.Http.Headers;
#nullable disable

namespace ShoeStore_Nhom2_Web2.Repositories
{
	public class ShopRepository : IShopRepository
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpContextAccessor _contextAccessor;

		public ShopRepository(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://localhost:7213");
			_contextAccessor = httpContextAccessor;
		}

		private void SetBearerToken()
		{
			var token = _contextAccessor.HttpContext.Session.GetString("JWTToken");
			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
		}

		public async Task<HttpResponseMessage> RegisterUserAsync(RegisterRequestDTO requestDTO)
		{
			requestDTO.Roles = new string[] { "Read" };
			return await _httpClient.PostAsJsonAsync("api/User/Register", requestDTO);
		}

		public async Task<HttpResponseMessage> LoginUserAsync(LoginViewModel loginModel)
		{
			return await _httpClient.PostAsJsonAsync("api/User/Login", loginModel);
		}

		public async Task<HttpResponseMessage> GetFilteredShoesAsync(string brand, string color)
		{
			SetBearerToken();
			return await _httpClient.GetAsync($"api/Shoe/get-shoe-F?brand={brand}&color={color}");
		}

		public async Task<HttpResponseMessage> GetShoesByPageAsync(int page, int pageSize)
		{
			SetBearerToken();
			return await _httpClient.GetAsync($"api/Shoe/get-shoe-P?page={page}&pageSize={pageSize}");
		}

		public async Task<HttpResponseMessage> GetShoeDetailAsync(int id)
		{
			SetBearerToken();
			return await _httpClient.GetAsync($"api/Shoe/{id}");
		}
	}
}
