using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaroohaNewSite.Data.Models
{
    public class Tbl_Menu : BaseEntity<string>
    {
        public Tbl_Menu()
        {
            ID = Guid.NewGuid().ToString();
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        [Required]
        public string subMenuName { get; set; }
        //===========================================================
        [Required]
        public string MenuImage { get; set; }
        //===========================================================
        public virtual ICollection<Tbl_Product> Tbl_Products { get; set; }
    }
}
