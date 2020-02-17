using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;

namespace ConfigSolution
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly IList<CultureInfo> m_Languages = new List<CultureInfo>();
        public static IList<CultureInfo> Languages => m_Languages;
        public App()
        {
            m_Languages.Clear();
            m_Languages.Add(new CultureInfo("ru-RU"));
            //Default culture Russia if you need new culture add dictionary and items this
        }
        public static event EventHandler LanguageChanged;

    }
}
