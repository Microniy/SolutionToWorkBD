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

namespace ClientDrawingArxiveWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CommandBinding bind_command_open = new CommandBinding(ApplicationCommands.Open);
        DrawingArxiveService.DrawingArxiveService1Client arxiveService1Client;
        public MainWindow()
        {
            InitializeComponent();
            arxiveService1Client = new DrawingArxiveService.DrawingArxiveService1Client();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bind_command_open.Executed += Bind_command_open_Executed;
            if(!CommandBindings.Contains(bind_command_open))
            CommandBindings.Add(bind_command_open);
        }

        private void Bind_command_open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CommandBindings.Contains(bind_command_open))
                CommandBindings.Remove(bind_command_open);
            var drawingItems = arxiveService1Client.GetWrongDrawingList();
            DrawingListVisual.ItemsSource = drawingItems;
           
            if (!CommandBindings.Contains(bind_command_open))
                CommandBindings.Add(bind_command_open);
        }
    }
}
