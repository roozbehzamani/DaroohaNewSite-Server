using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Repo.Infrastructure;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DaroohaNewSite.Common.Helpers;

namespace DaroohaNewSite.Repo.Repositories.Repo
{
    public class UserRepository : Repository<Tbl_User>, IUserRepository
    {
        private readonly DbContext _db;
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            _db ??= (DaroohaDbContext)_db;
        }

        public async Task<bool> UserExists(string Username, string UserType)
        {
            var user = await GetAsync(p => p.MobPhone.Equals(Username));
            if (user != null)
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
