using System.ComponentModel.DataAnnotations;

namespace DaroohaNewSite.Data.Dtos.Site.Panel.UserAddress
{
    public class UserAddressForUpdateDTO
    {
        [Required]
        public string AddressName { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
