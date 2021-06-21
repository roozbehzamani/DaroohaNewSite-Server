using DaroohaNewSite.Common.Helpers;
using DaroohaNewSite.Common.Helpers.Interface;
using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Repo.Infrastructure;
using DaroohaNewSite.Services.Site.Admin.Auth.Interface;
using System.Threading.Tasks;

namespace DaroohaNewSite.Services.Site.Admin.Auth.Service
{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork<DaroohaDbContext> _db;
        private readonly IUtilities _utilities;
        public AuthService(IUnitOfWork<DaroohaDbContext> dbContext, IUtilities utilities)
        {
            _db = dbContext;
            _utilities = utilities;
        }

        public async Task<Tbl_User> LoginAsync(string username, string password)
        {
            var user = await _db.UserRepository.GetAsync(p => p.MobPhone.Equals(username));

            if (user == null)
            {
                return null;
            }

            return user;
        }
        public async Task<Tbl_User> RegisterAsync(Tbl_User user)
        {
            byte[] PasswordHash, PasswordSalt;
            _utilities.CreatePasswordHash(user.Password, out PasswordHash, out PasswordSalt);

            //user.PasswordHash = PasswordHash;
            //user.PasswordSalt = PasswordSalt;

            await _db.UserRepository.InsertAsync(user);
            if (await _db.SaveAsync())
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> AddUserPreNeededAsync(Notification notify)
        {
            await _db.NotificationRepository.InsertAsync(notify);
            if (await _db.SaveAsync())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
