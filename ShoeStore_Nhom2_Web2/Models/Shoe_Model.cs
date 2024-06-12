#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ShoeStore_Nhom2_Web2.Models
{
	public class Shoe_Model
	{
		[Key]
		public int Id { get; set; }
		public string ImageUrl {  get; set; }
		public string Name { get; set; }
		public float Price {  get; set; }
		public float Old_Price {  get; set; }
		public string Brand {  get; set; }
		public string Color {  get; set; }
		public int Size {  get; set; }
		public int Remaining {  get; set; }
	}
}
