using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Repo.Infrastructure;
using DaroohaNewSite.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DaroohaNewSite.Repo.Repositories.Repo
{
    public class UserAddressRepository : Repository<Tbl_UserAddress>, IUserAddressRepository
    {
        private readonly DbContext _db;
        public UserAddressRepository(DbContext dbContext) : base(dbContext)
        {
            _db ??= (DaroohaDbContext)_db;
        }
    }
}
