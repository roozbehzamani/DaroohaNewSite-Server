﻿using DaroohaNewSite.Data.Models;
using System.Threading.Tasks;

namespace DaroohaNewSite.Services.Site.Admin.Auth.Interface
{
    public interface IAuthService
    {
        Task<Tbl_User> RegisterAsync(Tbl_User user);
        Task<Tbl_User> LoginAsync(string Username, string Password);
        Task<bool> AddUserPreNeededAsync(Notification notify);
    }
}
