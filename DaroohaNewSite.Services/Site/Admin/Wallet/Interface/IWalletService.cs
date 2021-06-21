using DaroohaNewSite.Common.ErrorsAndMessages;
using System.Threading.Tasks;

namespace DaroohaNewSite.Services.Site.Admin.Wallet.Interface
{
    public interface IWalletService
    {
        Task<bool> CheckInventoryAsync(int cost, string userID);
        Task<ReturnErrorMessage> DecreaseInventoryAsync(int cost, string userID);
        Task<ReturnErrorMessage> IncreaseInventoryAsync(int cost, string userID);
    }
}
