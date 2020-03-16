using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryServer
{
    public static class LocalDb
    {
        private const string CRYPTED_KEY = "KeyCripted"; //this key to be equal to key in ConfigSolution. You could be have a some keys.
        private readonly static Keeper keeper = Keeper.Instance(Keepers.Reg);
        private static string DataServer = StringCipher.Decrypt(keeper.GetParam("Parameter_1"),CRYPTED_KEY);    // Names parameters or key crypte may be any for you
        private static string NameBase = StringCipher.Decrypt(keeper.GetParam("Parameter_2"), CRYPTED_KEY);     //
        private static string Login = StringCipher.Decrypt(keeper.GetParam("Parameter_3"), CRYPTED_KEY);        //
        private static string Pass = StringCipher.Decrypt(keeper.GetParam("Parameter_4"), CRYPTED_KEY);         //
        
        private static System.Data.SqlClient.SqlConnectionStringBuilder connectBuildStr = new System.Data.SqlClient.SqlConnectionStringBuilder // For SQL
        {
            DataSource = DataServer,
            InitialCatalog = NameBase,
            UserID = Login,
            Password = Pass
        };
        private static System.Data.IDbConnection connect = new System.Data.SqlClient.SqlConnection { ConnectionString = connectBuildStr.ConnectionString}; // too for SQL
    }
}
