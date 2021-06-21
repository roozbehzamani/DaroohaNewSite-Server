using DaroohaNewSite.Repo.Repositories.Interface;
using DaroohaNewSite.Repo.Repositories.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DaroohaNewSite.Repo.Infrastructure
{
    public interface IUnitOfWork<TContext> :IDisposable where TContext : DbContext
    {
        bool Save();
        Task<bool> SaveAsync();
        IUserRepository UserRepository { get; }
        ISettingRepository SettingRepository { get; }
        IRoleRepository RoleRepository { get; }
        ITokenRepository TokenRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IUserAddressRepository UserAddressRepository { get; }
        IWalletRepository WalletRepository { get; }
        ITicketRepository TicketRepository { get; }
        ITicketContentRepository TicketContentRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IMenuRepository MenuRepository { get; }
        IHomeFirstSliderRepository HomeFirstSliderRepository { get; }
        ICommentRepository CommentRepository { get; }
        IBrandRepository BrandRepository { get; }
    }
}
