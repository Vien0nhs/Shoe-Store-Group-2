#nullable disable
namespace ShoeStore_Nhom2_Web2_API.Models
{
	public class ShoeCreateDto
	{
		public string ImageUrl { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public float Old_Price { get; set; }
		public string Brand { get; set; }
		public string Color { get; set; }
		public int Size { get; set; }
		public int Remaining { get; set; }
	}
}
