using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DaroohaNewSite.Data.Dtos.Site.Panel.Wallet
{
    public class WalletInventoryForUpdateDTO
    {
        [Required]
        public int Inventory { get; set; }
    }
}
