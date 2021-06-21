using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DaroohaNewSite.Data.Dtos.Site.Panel.Admin.Brand
{
    public class BrandForUpdateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 0)]
        public string BrandName { get; set; }
        public IFormFile File { get; set; }
    }
}
