using System.ComponentModel.DataAnnotations;
#nullable disable
namespace ShoeStore_Nhom2_Web2_API.Models
{
    public class RegisterRequestDTO
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordConfirm {  get; set; }
        public string[] Roles { get; set; }
    }
}
