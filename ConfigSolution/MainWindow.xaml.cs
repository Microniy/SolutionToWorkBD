﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RepositoryServer;

namespace ConfigSolution
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly CommandBinding bind_Open = new CommandBinding(ApplicationCommands.Open);
        readonly CommandBinding bind_Save = new CommandBinding(ApplicationCommands.Save);
        private const string CRYPTED_KEY = "KeyCripted";//for example key crypted
        private readonly Keeper keeper;
        private readonly Keeper log;
        private readonly ConfigurationDataClass ParamServer = new ConfigurationDataClass();
        private delegate void SetMetod(string value);
        public MainWindow()
        {
            InitializeComponent();
            bind_Open.Executed += Bind_Open_Executed;
            bind_Save.Executed += Bind_Save_Executed;
           
            keeper = Keeper.Instance(Keepers.Reg);
            log = Keeper.Instance(Keepers.Log);

            this.CommandBindings.Add(bind_Open);
            GetParamServer();
            this.DataContext = ParamServer;
        }

        private void Bind_Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SetParamServer();
            if (CommandBindings.Contains(bind_Save)) { CommandBindings.Remove(bind_Save); }
        }

        private void GetParamServer() //get save parameters to binding
        {
             
            void GetParam(string paramName, SetMetod objParam)
            {
                string param1 = keeper.GetParam(paramName);
                if (string.IsNullOrEmpty(param1))
                {
                    objParam(string.Empty);
                }
                else
                {
                    objParam (StringCipher.Decrypt(param1, CRYPTED_KEY));
                }
            }

            GetParam("Parameter_1", ParamServer.SetNameServer);
            GetParam("Parameter_2", ParamServer.SetNameBase);
            GetParam("Parameter_3", ParamServer.SetLogin);
            ParamServer.IndexRow = keeper.GetParam("IndexRow");
        }
        private void SetParamServer() //set save parameters to binding
        {

            void SetParam(string paramName, string value)
            {
                string criptStr = StringCipher.Encrypt(value, CRYPTED_KEY);
               if(!keeper.Save(paramName, criptStr))
                {
                    log.Save(paramName, "This parameter don't saved");
                }
               
            }
            //Names and positions parameters best be changed in your project
            SetParam("Parameter_1", ParamServer.NameServer);
            SetParam("Parameter_2", ParamServer.NameBase);
            SetParam("Parameter_3", ParamServer.Login);
            SetParam("Parameter_4", PassInputBox.Password);
            keeper.Save("IndexRow", ParamServer.IndexRow);
            PassInputBox.Password = string.Empty;
        }

        private void Bind_Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {           
            // this connection for example SQL base
            System.Data.IDbConnection connect = new System.Data.SqlClient.SqlConnection();
            System.Data.SqlClient.SqlConnectionStringBuilder connectBuildStr = new System.Data.SqlClient.SqlConnectionStringBuilder
            {
                DataSource = ParamServer.NameServer,
                InitialCatalog = ParamServer.NameBase,
                UserID = ParamServer.Login,
                Password = PassInputBox.Password
            };
            connect.ConnectionString = connectBuildStr.ConnectionString;
            try
            {
                connect.Open();
                MessageBox.Show("Test connection correct");
            }

            catch (System.Data.Common.DbException err)
            {
                log.Save("Test connection error", err.Message);
                MessageBox.Show("Test connection wrong","Waiting",MessageBoxButton.OK,MessageBoxImage.Error);
            }

            finally
            {
                connect.Close();
            }
            try
            {
                RepositoryServer.LocalDb.TestMetod();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.StackTrace);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!CommandBindings.Contains(bind_Save)) { CommandBindings.Add(bind_Save); } // Add command save because properties changed
            System.Windows.Media.Animation.DoubleAnimation showAnimation = new System.Windows.Media.Animation.DoubleAnimation
            {
                From = stackButtons.ActualHeight,
                To = 30,
                Duration = TimeSpan.FromSeconds(1)
            };
            stackButtons.BeginAnimation(StackPanel.HeightProperty, showAnimation);
        }

        private void PassBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace((e.Source as PasswordBox).Password))
            {
                if (!CommandBindings.Contains(bind_Save)) { CommandBindings.Add(bind_Save); } // Add command save because properties changed
                System.Windows.Media.Animation.DoubleAnimation showAnimation = new System.Windows.Media.Animation.DoubleAnimation
                {
                    From = stackButtons.ActualHeight,
                    To = 30,
                    Duration = TimeSpan.FromSeconds(1)
                };
                stackButtons.BeginAnimation(StackPanel.HeightProperty, showAnimation);
            }
            else
            {
                if (CommandBindings.Contains(bind_Save)) { CommandBindings.Remove(bind_Save); } //I don't want save for password to nothing

            }
        }
    }
}
