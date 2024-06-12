using PlantShield.Dtos;

namespace PlantShield.Services
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterAsync(RegisterModel model);
        Task<AuthDto> GetTokenAsync(TokenRequestDto model);
        Task<string> AddRoleAsync(AddUserToRoleDto model);
    }
}
