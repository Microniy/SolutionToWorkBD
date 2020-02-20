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

namespace ConfigSolution
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CommandBinding bind_Open = new CommandBinding(ApplicationCommands.Open);
        public MainWindow()
        {
            InitializeComponent();
            bind_Open.Executed += Bind_Open_Executed;
            this.CommandBindings.Add(bind_Open);
        }

        private void Bind_Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string criptStr = StringCipher.Encrypt("1111", "KeyCripted");
            MessageBox.Show(criptStr);
            MessageBox.Show(StringCipher.Decrypt(criptStr, "KeyCripted"));
        }
    }
}
