using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPYSLSAILORAPP.Infrastructure.Identity.General
{
    public class IdentitySettings
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }
}
