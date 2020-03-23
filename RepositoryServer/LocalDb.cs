using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace RepositoryServer
{
    public static partial class LocalDb
    {
        private const string CRYPTED_KEY = "KeyCripted"; //this key to be equal to key in ConfigSolution. You could be have a some keys.
        private readonly static Keeper keeper = Keeper.Instance(Keepers.Reg);
        private readonly static Keeper log = Keeper.Instance(Keepers.Log);
        private static readonly string DataServer = StringCipher.Decrypt(keeper.GetParam("Parameter_1"),CRYPTED_KEY);    // Names parameters or key crypte may be any for you
        private static readonly string NameBase = StringCipher.Decrypt(keeper.GetParam("Parameter_2"), CRYPTED_KEY);     //
        private static readonly string Login = StringCipher.Decrypt(keeper.GetParam("Parameter_3"), CRYPTED_KEY);        //
        private static readonly string Pass = StringCipher.Decrypt(keeper.GetParam("Parameter_4"), CRYPTED_KEY);         //
        private static int IndexRow = Convert.ToInt32( keeper.GetParam("IndexRow"));
        private static readonly object CountConnectedLock = new object();
        private static int CountConnected = 0;       
        public static event EventHandler DataUpdated;

        private static SqlConnectionStringBuilder connectBuildStr = new SqlConnectionStringBuilder // For SQL
        {
            DataSource = DataServer,
            InitialCatalog = NameBase,
            UserID = Login,
            Password = Pass,
            MultipleActiveResultSets = true
        };
        private static System.Data.IDbConnection connect = new SqlConnection { ConnectionString = connectBuildStr.ConnectionString}; // too for SQL
                                                                                                                                     
        private static System.Data.DataTable table = new System.Data.DataTable();
        public static System.Data.DataTable TableWrongDrawings => table;
        public static void TestMetod()
        {
          
        }
        public static async void UpdateDataAsync() //This method should be rewritten to for your task
        {
            IndexRow = Convert.ToInt32(keeper.GetParam("IndexRow"));
            SqlCommand dbCommand1 = new SqlCommand
            {
                CommandText = "GetErrorDrawingsArchivation",
                Connection = connect as SqlConnection,
                CommandType = System.Data.CommandType.StoredProcedure,
                CommandTimeout = 3600
            };
           System.Data.IDataParameter ParamInput = new SqlParameter { ParameterName= "@LastIndexError", Value = IndexRow };
            dbCommand1.Parameters.Add(ParamInput);
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
                   lock(CountConnectedLock)
                    {
                        if(CountConnected == 0)
                        {
                            connect.Open();
                        }
                        CountConnected++;
                    }
                    

                    System.Data.IDataReader dataReader = dbCommand1.ExecuteReader();
                    
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
                    log.Save("Task Reader errors",err.Message);
                }
                finally
                {

                    lock (CountConnectedLock)
                    {
                        if (CountConnected == 1)
                        {
                            connect.Close();
                        }
                        CountConnected--;
                    }
                   
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
            DataUpdateEventArgs eventArgs = new DataUpdateEventArgs { Count = CountTable };
            
            DataUpdated?.Invoke(null, eventArgs);
        }
        public static async void UpdateDrawingAsync(int id) //This method should be rewritten to for your task
        {
            SqlCommand dbCommand2 = new SqlCommand
            {
                CommandText = "SetErrorDrawingsArchivation",
                Connection = connect as SqlConnection,
                CommandType = System.Data.CommandType.StoredProcedure,
                CommandTimeout = 500
            };
            System.Data.IDataParameter ParamInput = new SqlParameter { ParameterName = "@NumberDoc", Value = id };
            dbCommand2.Parameters.Add(ParamInput);
            Task<bool> task1 = Task<bool>.Factory.StartNew(() =>
            {
                bool IsCorrect = true;
                try
                {
                    lock (CountConnectedLock)
                    {
                        if (CountConnected == 0)
                        {
                            connect.Open();
                        }
                        CountConnected++;
                    }
                    _ = dbCommand2.ExecuteNonQuery();
                   
                }
                catch (Exception err)
                {
                    IsCorrect = false;
                    log.Save("Update drawing Task Reader errors", err.Message);
                }
                finally
                {

                    lock (CountConnectedLock)
                    {
                        if (CountConnected == 1)
                        {
                            connect.Close();
                        }
                        CountConnected--;
                    }
                }
                return IsCorrect;
            });
            bool fone = await task1;
            System.Diagnostics.Debug.WriteLine(string.Format("SetErrorDrawingsArchivation status = {0}",fone));
        }
    }
}
