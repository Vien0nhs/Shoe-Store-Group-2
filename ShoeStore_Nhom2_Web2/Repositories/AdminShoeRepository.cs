using ShoeStore_Nhom2_Web2.Models;
#nullable disable
using System.Net.Http.Headers;

namespace ShoeStore_Nhom2_Web2.Repositories
{
	public class AdminShoeRepository : IAdminShoeRepository
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpContextAccessor _contextAccessor;

		public AdminShoeRepository(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
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

		public async Task<HttpResponseMessage> GetAllShoesAsync()
		{
			SetBearerToken();
			return await _httpClient.GetAsync("api/Shoe/get-shoe");
		}

		public async Task<HttpResponseMessage> GetShoeByIdAsync(int id)
		{
			SetBearerToken();
			return await _httpClient.GetAsync($"api/Shoe/{id}");
		}

		public async Task<HttpResponseMessage> CreateShoeAsync(ShoeCreateDto shoeCreateDto)
		{
			SetBearerToken();
			return await _httpClient.PostAsJsonAsync("api/Shoe", shoeCreateDto);
		}

		public async Task<HttpResponseMessage> UpdateShoeAsync(int id, Shoe_Model shoeModel)
		{
			SetBearerToken();
			return await _httpClient.PutAsJsonAsync($"api/Shoe/{id}", shoeModel);
		}

		public async Task<HttpResponseMessage> DeleteShoeAsync(int id)
		{
			SetBearerToken();
			return await _httpClient.DeleteAsync($"api/Shoe/{id}");
		}

	}
}
