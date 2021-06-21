using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Repo.Infrastructure;
using DaroohaNewSite.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DaroohaNewSite.Repo.Repositories.Repo
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        private readonly DbContext _db;
        public TokenRepository(DbContext dbContext) : base(dbContext)
        {
            _db ??= (DaroohaDbContext)_db;
        }
    }
}
