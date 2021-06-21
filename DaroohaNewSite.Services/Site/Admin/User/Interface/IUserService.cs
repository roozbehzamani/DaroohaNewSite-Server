using DaroohaNewSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaroohaNewSite.Services.Site.Admin.User.Interface
{
    public interface IUserService
    {
        Task<Tbl_User> GetUserForPassChange(string id, string Password);
        Task<bool> UpdateUserPassword(Tbl_User user, string NewPassword);
    }
}
