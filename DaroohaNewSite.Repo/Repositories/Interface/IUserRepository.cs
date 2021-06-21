using DaroohaNewSite.Repo.Infrastructure;
using DaroohaNewSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaroohaNewSite.Repo.Repositories.Interface
{
    public interface IUserRepository : IRepository<Tbl_User>
    {
        Task<bool> UserExists(string Username, string UserType);
    }
}
