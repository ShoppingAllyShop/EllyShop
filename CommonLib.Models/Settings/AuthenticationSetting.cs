using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Models.Settings
{
    public class AuthenticationSetting
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public AccessTokenExperiedTime AccessTokenExperiedTime { get; set; }
        public RefreshTokenExperiedTime RefreshTokenExperiedTime { get; set; }
    }
    public class AccessTokenExperiedTime
    {
        public int ClientPage { get; set; }
        public int AdminPage { get; set; }

    }
    public class RefreshTokenExperiedTime
    {
        public int ClientPage { get; set; }
        public int AdminPage { get; set; }
    }
}