﻿using DaroohaNewSite.Data.Dtos.Site.Panel.User;

namespace DaroohaNewSite.Data.Dtos.Site.Panel.Auth
{
    public class LoginResponseDTO
    {
        public string token { get; set; }
        public string refresh_token { get; set; }
        public UserForDetailedDTO user { get; set; }
    }
}