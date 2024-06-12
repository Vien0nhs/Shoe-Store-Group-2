using ShoeStore_Nhom2_Web2.Models;
namespace ShoeStore_Nhom2_Web2.Repositories
{
	public interface IAdminShoeRepository
	{
		Task<HttpResponseMessage> GetAllShoesAsync();
		Task<HttpResponseMessage> GetShoeByIdAsync(int id);
		Task<HttpResponseMessage> CreateShoeAsync(ShoeCreateDto shoeCreateDto);
		Task<HttpResponseMessage> UpdateShoeAsync(int id, Shoe_Model shoeModel);
		Task<HttpResponseMessage> DeleteShoeAsync(int id);

	}
}
