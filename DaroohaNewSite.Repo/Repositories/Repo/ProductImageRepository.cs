using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Repo.Infrastructure;
using DaroohaNewSite.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DaroohaNewSite.Repo.Repositories.Repo
{
    public class ProductImageRepository : Repository<Tbl_ProductImage>, IProductImageRepository
    {
        private readonly DbContext _db;
        public ProductImageRepository(DbContext dbContext) : base(dbContext)
        {
            _db ??= (DaroohaDbContext)_db;
        }

    }
}