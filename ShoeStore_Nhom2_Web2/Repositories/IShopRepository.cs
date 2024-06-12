using ShoeStore_Nhom2_Web2.Models;
namespace ShoeStore_Nhom2_Web2.Repositories
{
	public interface IShopRepository
	{
		Task<HttpResponseMessage> RegisterUserAsync(RegisterRequestDTO requestDTO);
		Task<HttpResponseMessage> LoginUserAsync(LoginViewModel loginModel);
		Task<HttpResponseMessage> GetFilteredShoesAsync(string brand, string color);
		Task<HttpResponseMessage> GetShoesByPageAsync(int page, int pageSize);
		Task<HttpResponseMessage> GetShoeDetailAsync(int id);
	}
}
