using System.ComponentModel.DataAnnotations;
#nullable disable
namespace ShoeStore_Nhom2_Web2_API.Models
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username {  get; set; }
        [Required]
        public string Password { get; set; }
    }
}
