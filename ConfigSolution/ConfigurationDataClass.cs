using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigSolution
{
    public class ConfigurationDataClass
    {       
        private string _nameServer;
        public string NameServer { get => _nameServer; set => _nameServer = value; }       
        public void SetNameServer(string name)
        {
            _nameServer = name;
        }
        private string _nameBase;
        public string NameBase { get => _nameBase; set => _nameBase = value; }
        public void SetNameBase(string name)
        {
            _nameBase = name;
        }
        private string _login;
        public string Login { get => _login; set => _login = value; }
        public void SetLogin(string name)
        {
            _login = name;
        }
        private string _indexRow;
        public string IndexRow { get => _indexRow; set => _indexRow = value; }
        public void SetIndexRow(int index)
        {
            _indexRow = index.ToString();
        }
        public ConfigurationDataClass()
        {
            NameServer = string.Empty;
            NameBase = string.Empty;
            Login = string.Empty;
            IndexRow = "0";
        }
    }
}
