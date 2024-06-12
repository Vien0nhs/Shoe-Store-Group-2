#nullable disable
namespace ShoeStore_Nhom2_Web2.Models
{
    public class LoginResponseDTO
    {
        public string JwtToken { set; get; }
        public List<string> Roles { set; get; }

    }
}
