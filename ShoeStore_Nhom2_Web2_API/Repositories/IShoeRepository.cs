using ShoeStore_Nhom2_Web2_API.Models;

namespace ShoeStore_Nhom2_Web2_API.Repositories
{
	public interface IShoeRepository
	{
		Task<IEnumerable<Shoe_Model>> GetShoes();
		Task<IEnumerable<Shoe_Model>> GetShoesPaged(int page, int pageSize);
		Task<IEnumerable<Shoe_Model>> GetFilteredShoes(string brand, string color);
		Task<Shoe_Model> GetShoeById(int id);
		Task<Shoe_Model> AddShoe(Shoe_Model shoeModel);
		Task<Shoe_Model> UpdateShoe(Shoe_Model shoeModel);
		Task<bool> DeleteShoe(int id);
		bool ShoeExists(int id);
	}
}
