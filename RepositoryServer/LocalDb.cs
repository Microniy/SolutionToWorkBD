using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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
        private static int IndexRow = Convert.ToInt32( keeper.GetParam("IndexRow"));
        
        private static SqlConnectionStringBuilder connectBuildStr = new SqlConnectionStringBuilder // For SQL
        {
            DataSource = DataServer,
            InitialCatalog = NameBase,
            UserID = Login,
            Password = Pass
        };
        private static System.Data.IDbConnection connect = new SqlConnection { ConnectionString = connectBuildStr.ConnectionString}; // too for SQL
                                                                                                                                     // private static System.Data.DataSet db = new System.Data.DataSet ();
        private static System.Data.DataTable table = new System.Data.DataTable();
        public static System.Data.DataTable TableWrongDrawings => table;
        public static void TestMetod()
        {
            System.Diagnostics.Debug.WriteLine("TEST CORRECT");
        }
        public static async void UpdateDataAsync()
        {
            SqlCommand dbCommand = new SqlCommand
            {
                CommandText = "GetErrorDrawingsArchivation",
                Connection = connect as SqlConnection,
                CommandType = System.Data.CommandType.StoredProcedure           
            };
           System.Data.IDataParameter ParamInput = new SqlParameter { ParameterName= "@LastIndexError", Value = IndexRow };
           dbCommand.Parameters.Add(ParamInput);
            Task<System.Data.DataTable> task1 = Task<System.Data.DataTable>.Factory.StartNew(() =>
            {
                System.Data.DataTable TaskTable = new System.Data.DataTable();
                System.Data.DataColumn dataColumn1 = new System.Data.DataColumn("ID");
                System.Data.DataColumn dataColumn2 = new System.Data.DataColumn("NameFile");
                System.Data.DataColumn dataColumn3 = new System.Data.DataColumn("IDLast");
                TaskTable.Columns.Add(dataColumn1);
                TaskTable.Columns.Add(dataColumn2);
                TaskTable.Columns.Add(dataColumn3);
                try
                {
                    
                    connect.Open();

                    System.Data.IDataReader dataReader = dbCommand.ExecuteReader();
                    
                    while (dataReader.Read())
                    {
                        var newRow = TaskTable.Rows.Add();
                        foreach (System.Data.DataColumn col in TaskTable.Columns)
                        {
                            newRow[col.ColumnName] = dataReader[col.ColumnName];
                        }
                    }
                    System.Diagnostics.Debug.WriteLine(TaskTable.Rows[0][0].ToString());
                    System.Diagnostics.Debug.WriteLine(TaskTable.Rows[0][1].ToString());
                    System.Diagnostics.Debug.WriteLine(TaskTable.Rows[0][2].ToString());
                }
                catch (Exception err)
                {
                    System.Diagnostics.Debug.WriteLine(err.Message);
                }
                finally
                {

                    connect.Close();
                }
                return TaskTable;
            });

            table = await task1;
            int CountTable = table.Rows.Count;
            if(CountTable > 0)
            {
                keeper.Save("IndexRow", table.Rows[0][2].ToString());
            }
            System.Diagnostics.Debug.WriteLine(CountTable);
        }
    }
}
