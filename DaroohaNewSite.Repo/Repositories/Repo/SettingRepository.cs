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
    public class SettingRepository : Repository<Tbl_Setting>, ISettingRepository
    {
        private readonly DbContext _db;
        public SettingRepository(DbContext dbContext) : base(dbContext)
        {
            _db ??= (DaroohaDbContext)_db;
        }
    }
}
