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
        static App()
        {            
            m_Languages.Add(new CultureInfo("ru-RU"));
            //m_Languages.Add(new CultureInfo("en-US"));
            //Default culture Russia if you need new culture add dictionary and items this
        }
        public App()
        {      
           
        }
        public static event EventHandler LanguageChanged;
        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();

                LanguageSwith.FindDictionary(value.Name, ref dict);
               
                // find current dictionary
                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                              select d).First();
                // change dictionary
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);

                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                LanguageChanged(Application.Current, new EventArgs());
            }
        }

    }
    public static class LanguageSwith
    {
        private static string TemplateMethod(string name,ref ResourceDictionary dict,bool IsNotTest = true)
        {
            switch (name)
            {
                /* this template for input new culture. Dictionary new language to need name for example: lang.en-US.xaml
                case "en-US":
                   if(IsNotTest) dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                    return name; // "break" changed return name for unit test
                    */
                default:
                   if(IsNotTest)  dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                    return "ru-RU";
            }
        }
        public static void FindDictionary(string name,ref ResourceDictionary dict) // All change only templated method
        {
            _ = TemplateMethod(name,ref dict);
        }
        public static string FindDictionary(string name) //For unit test
        {
            ResourceDictionary dict = new ResourceDictionary();
            return TemplateMethod(name, ref dict,false);
        }
    }
}
