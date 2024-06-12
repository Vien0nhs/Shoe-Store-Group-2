using Microsoft.EntityFrameworkCore;
using ShoeStore_Nhom2_Web2_API.Data;
using ShoeStore_Nhom2_Web2_API.Models;
#nullable disable
namespace ShoeStore_Nhom2_Web2_API.Repositories
{
	public class ShoeRepository : IShoeRepository
	{
		private readonly Shoe_DbContext _context;

		public ShoeRepository(Shoe_DbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Shoe_Model>> GetShoes()
		{
			return await _context.Shoe.ToListAsync();
		}

		public async Task<IEnumerable<Shoe_Model>> GetShoesPaged(int page, int pageSize)
		{
			return await _context.Shoe
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<IEnumerable<Shoe_Model>> GetFilteredShoes(string brand, string color)
		{
			var query = _context.Shoe.AsQueryable();

			if (!string.IsNullOrEmpty(brand))
			{
				query = query.Where(s => s.Brand.Contains(brand));
			}

			if (!string.IsNullOrEmpty(color))
			{
				query = query.Where(s => s.Color.Contains(color));
			}

			return await query.ToListAsync();
		}

		public async Task<Shoe_Model> GetShoeById(int id)
		{
			return await _context.Shoe.FindAsync(id);
		}

		public async Task<Shoe_Model> AddShoe(Shoe_Model shoeModel)
		{
			_context.Shoe.Add(shoeModel);
			await _context.SaveChangesAsync();
			return shoeModel;
		}

		public async Task<Shoe_Model> UpdateShoe(Shoe_Model shoeModel)
		{
			_context.Entry(shoeModel).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return shoeModel;
		}

		public async Task<bool> DeleteShoe(int id)
		{
			var shoeModel = await _context.Shoe.FindAsync(id);
			if (shoeModel == null)
			{
				return false;
			}

			_context.Shoe.Remove(shoeModel);
			await _context.SaveChangesAsync();
			return true;
		}

		public bool ShoeExists(int id)
		{
			return _context.Shoe.Any(e => e.Id == id);
		}
	}
}
