using System;
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

namespace ConfigSolution
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly CommandBinding bind_Open = new CommandBinding(ApplicationCommands.Open);
        private const string CRYPTED_KEY = "KeyCripted";//for example key crypted
        private Keeper keeper;
        private ConfigurationDataClass ParamServer;
        public MainWindow()
        {
            InitializeComponent();
            bind_Open.Executed += Bind_Open_Executed;
            this.CommandBindings.Add(bind_Open);
            keeper = Keeper.Instance(Keepers.Reg);

        }
        private void GetParamServer()
        {
            /*string param1 = keeper.GetParam("Parameter_1");
            if (string.IsNullOrEmpty(param1))
            {
                ParamServer.NameServer = string.Empty;
            }
            else
            {
                ParamServer.NameServer = StringCipher.Decrypt(param1, CRYPTED_KEY);
            }
            */ //need to be method
            string param2 = keeper.GetParam("Parameter_2");
            if (string.IsNullOrEmpty(param2))
            {
                ParamServer.NameBase = string.Empty;
            }
            else
            {
                ParamServer.NameBase = StringCipher.Decrypt(param2, CRYPTED_KEY);
            }
        }

        private void Bind_Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string criptStr = StringCipher.Encrypt("1111", CRYPTED_KEY);
            MessageBox.Show(criptStr);
            MessageBox.Show(StringCipher.Decrypt(criptStr, CRYPTED_KEY));
            try
            {
                keeper.Save("Parameter_1", criptStr);
                MessageBox.Show(keeper.GetParam("Parameter_1"));
            }
            catch(Exception err)
            {
                MessageBox.Show(err.StackTrace);
            }

        }
    }
}
