﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DaroohaNewSite.Data.Models
{
    public class Ticket : BaseEntity<string>
    {
        public Ticket()
        {
            ID = Guid.NewGuid().ToString();
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        [Required]
        [StringLength(50, MinimumLength = 0)]
        public string Title { get; set; }
        [Required]
        public bool Closed { get; set; }
        [Required]
        public short Department { get; set; }
        [Required]
        public short Level { get; set; }
        [Required]
        public bool IsAdminSide { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Tbl_User User { get; set; }

        public virtual ICollection<TicketContent> TicketContents { get; set; }
    }
}