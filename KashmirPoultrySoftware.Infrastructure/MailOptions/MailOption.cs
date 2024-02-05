using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Infrastructure.MailSetting
{
    public class MailOption
    {
        public string ApiKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string FromName { get; set; } = null!;
        public string FromEmail { get; set; } = null!;
    }
}
