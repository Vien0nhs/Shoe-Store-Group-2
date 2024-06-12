#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ShoeStore_Nhom2_Web2_API.Models
{
	public class LoginViewModel
	{
		[Key]
		public int UserId { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
