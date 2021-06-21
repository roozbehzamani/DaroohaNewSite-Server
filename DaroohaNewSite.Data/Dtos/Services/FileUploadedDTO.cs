using System;
using System.Collections.Generic;
using System.Text;

namespace DaroohaNewSite.Data.Dtos.Services
{
    public class FileUploadedDTO
    {
        public bool Status { get; set; }
        public string Url { get; set; }
        public string PublicID { get; set; } = "0";
        public string Message { get; set; }
    }
}
