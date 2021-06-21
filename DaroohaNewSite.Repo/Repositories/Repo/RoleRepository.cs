﻿using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Repo.Infrastructure;
using DaroohaNewSite.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DaroohaNewSite.Repo.Repositories.Repo
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly DbContext _db;
        public RoleRepository(DbContext dbContext) : base(dbContext)
        {
            _db ??= (DaroohaDbContext)_db;
        }
    }
}
