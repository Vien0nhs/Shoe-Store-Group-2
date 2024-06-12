using Microsoft.AspNetCore.Identity;
using ShoeStore_Nhom2_Web2_API.Models;
#nullable disable

namespace ShoeStore_Nhom2_Web2_API.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ITokenRepository _tokenRepository;

		public UserRepository(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
		{
			_userManager = userManager;
			_tokenRepository = tokenRepository;
		}

		public async Task<IdentityResult> RegisterUserAsync(RegisterRequestDTO registerRequestDTO)
		{
			var identityUser = new IdentityUser
			{
				UserName = registerRequestDTO.Username,
				Email = registerRequestDTO.Username
			};

			if (registerRequestDTO.Password != registerRequestDTO.PasswordConfirm)
			{
				return IdentityResult.Failed(new IdentityError { Description = "Xác nhận mật khẩu không chính xác!" });
			}

			var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);

			if (identityResult.Succeeded && registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
			{
				identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
			}

			return identityResult;
		}

		public async Task<(bool IsSuccess, LoginResponseDTO LoginResponse, string Error)> LoginUserAsync(LoginRequestDTO loginRequestDTO)
		{
			var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);
			if (user != null)
			{
				var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
				if (checkPasswordResult)
				{
					var roles = await _userManager.GetRolesAsync(user);
					if (roles != null)
					{
						var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());
						var response = new LoginResponseDTO
						{
							JwtToken = jwtToken,
							Roles = roles.ToList()
						};

						return (true, response, null);
					}
				}
			}
			return (false, null, "Username or password incorrect");
		}
	}
}
