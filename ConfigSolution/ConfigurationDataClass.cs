using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigSolution
{
    public class ConfigurationDataClass
    {
        public string NameServer { get; set; }
        public string NameBase { get; set; }
        public string Login { get; set; }
        public ConfigurationDataClass()
        {
            NameServer = string.Empty;
            NameBase = string.Empty;
            Login = string.Empty;
        }
    }
}
