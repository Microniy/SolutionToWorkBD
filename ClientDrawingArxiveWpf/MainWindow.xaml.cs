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
        CommandBinding bind_command_save = new CommandBinding(ApplicationCommands.Save);
        DrawingArxiveService.DrawingArxiveService1Client arxiveService1Client;
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bind_command_open.Executed += Bind_command_open_Executed;
            bind_command_save.Executed += Bind_command_save_Executed;
            if(!CommandBindings.Contains(bind_command_open))
            CommandBindings.Add(bind_command_open);
        }

        private void Bind_command_save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using (arxiveService1Client = new DrawingArxiveService.DrawingArxiveService1Client())
            {
                if (CommandBindings.Contains(bind_command_save))
                    CommandBindings.Remove(bind_command_save);
                foreach (DrawingArxiveService.DrawingItem drawing in DrawingListVisual.SelectedItems)
                {

                    arxiveService1Client.SetRepeatArxivation(drawing.ID);
                    System.Diagnostics.Debug.WriteLine(drawing.Name);
                    System.Diagnostics.Debug.WriteLine("Отправлено");
                    Task.Delay(5000);
                }
                if (!CommandBindings.Contains(bind_command_save))
                    CommandBindings.Add(bind_command_save);
            }
        }

        private void Bind_command_open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (CommandBindings.Contains(bind_command_open))
                CommandBindings.Remove(bind_command_open);
            using (arxiveService1Client = new DrawingArxiveService.DrawingArxiveService1Client())
            {
                var drawingItems = arxiveService1Client.GetWrongDrawingList();
                DrawingListVisual.ItemsSource = drawingItems;
                if (drawingItems.Count > 0)
                {
                    if (!CommandBindings.Contains(bind_command_save))
                        CommandBindings.Add(bind_command_save);
                }
                else
                {
                    if (CommandBindings.Contains(bind_command_save))
                        CommandBindings.Remove(bind_command_save);
                }
            }
            if (!CommandBindings.Contains(bind_command_open))
                CommandBindings.Add(bind_command_open);
        }
    }
}
