using Microsoft.EntityFrameworkCore;
using ShoeStore_Nhom2_Web2_API.Models;

namespace ShoeStore_Nhom2_Web2_API.Data
{
	public class Shoe_DbContext: DbContext
	{
		public Shoe_DbContext(DbContextOptions<Shoe_DbContext> options) : base(options) { }
		public DbSet<Shoe_Model> Shoe { get; set; }
    }
}
