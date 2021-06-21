using DaroohaNewSite.Data.Dtos.Common.Token;
using DaroohaNewSite.Data.Models;
using System.Threading.Tasks;

namespace DaroohaNewSite.Common.Helpers.Interface
{
    public interface IUtilities
    {
        void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt);
        bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt);

        Task<TokenResponseDTO> GenerateNewTokenAsync(TokenRequestDTO tokenRequestDTO);
        Task<TokenResponseDTO> CreateAccessTokenAsync(Tbl_User user, string refreshToken);
        Token CreateRefreshToken(string clientId, string userId, bool isRemember);

        Task<TokenResponseDTO> RefreshAccessTokenAsync(TokenRequestDTO tokenRequestDto);
    }
}
