#nullable disable
using System.ComponentModel.DataAnnotations;

namespace ShoeStore_Nhom2_Web2.Models
{
	public class LoginViewModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
