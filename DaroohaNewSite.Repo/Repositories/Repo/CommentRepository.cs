using DaroohaNewSite.Data.DatabaseContext;
using DaroohaNewSite.Data.Models;
using DaroohaNewSite.Repo.Infrastructure;
using DaroohaNewSite.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DaroohaNewSite.Repo.Repositories.Repo
{
    public class CommentRepository : Repository<Tbl_Comment>, ICommentRepository
    {
        private readonly DbContext _db;
        public CommentRepository(DbContext dbContext) : base(dbContext)
        {
            _db ??= (DaroohaDbContext)_db;
        }

    }
}