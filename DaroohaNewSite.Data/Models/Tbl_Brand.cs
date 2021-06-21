using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DaroohaNewSite.Data.Models
{
    public class Tbl_Brand : BaseEntity<string>
    {
        public Tbl_Brand()
        {
            ID = Guid.NewGuid().ToString();
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        [Required]
        public string BrandName { get; set; }
        [Required]
        public string BrandLogoUrl { get; set; }
        //===========================================================
        public virtual ICollection<Tbl_Product> Tbl_Products { get; set; }
    }
}
