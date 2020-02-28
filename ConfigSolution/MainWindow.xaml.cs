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
        private ConfigurationDataClass ParamServer = new ConfigurationDataClass();
        private delegate void SetMetod(string value);
        public MainWindow()
        {
            InitializeComponent();
            bind_Open.Executed += Bind_Open_Executed;
            this.CommandBindings.Add(bind_Open);
            keeper = Keeper.Instance(Keepers.Reg);
            GetParamServer();
            this.DataContext = ParamServer;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Media.Animation.DoubleAnimation showAnimation = new System.Windows.Media.Animation.DoubleAnimation();
            showAnimation.From = stackButtons.ActualHeight;
            showAnimation.To = 30;
            showAnimation.Duration = TimeSpan.FromSeconds(1);
            stackButtons.BeginAnimation(StackPanel.HeightProperty, showAnimation);
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            System.Windows.Media.Animation.DoubleAnimation showAnimation = new System.Windows.Media.Animation.DoubleAnimation();
            showAnimation.From = stackButtons.ActualHeight;
            showAnimation.To = 30;
            showAnimation.Duration = TimeSpan.FromSeconds(1);
            stackButtons.BeginAnimation(StackPanel.HeightProperty, showAnimation);
        }
    }
}
