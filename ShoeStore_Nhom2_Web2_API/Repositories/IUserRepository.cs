using Microsoft.AspNetCore.Identity;
using ShoeStore_Nhom2_Web2_API.Models;

namespace ShoeStore_Nhom2_Web2_API.Repositories
{
	public interface IUserRepository
	{
		Task<IdentityResult> RegisterUserAsync(RegisterRequestDTO registerRequestDTO);
		Task<(bool IsSuccess, LoginResponseDTO LoginResponse, string Error)> LoginUserAsync(LoginRequestDTO loginRequestDTO);
	}
}
