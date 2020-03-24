using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using RepositoryServer;
using DrawingArxiveService;

namespace ConsoleHostServise
{
    class Program
    {
        static ServiceHost DrawingArxiveServiceHost;
        
        private static System.Timers.Timer UpdateDrawingTimer; 
        static void Main(string[] args)
        {
            Console.WriteLine("Update data started");
            RepositoryServer.LocalDb.DataUpdated += LocalDb_DataUpdated;
            UpdateDrawingTimer = new System.Timers.Timer(90000.0);
            UpdateDrawingTimer.Elapsed += UpdateDrawingTimer_Elapsed;
            RepositoryServer.LocalDb.UpdateDataAsync();           
            Console.WriteLine("ServiceHost");
            try
            {
                DrawingArxiveServiceHost = new ServiceHost(typeof(DrawingArxiveService.DrawingArxiveService));
                DrawingArxiveServiceHost.Open();
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
            Console.WriteLine("Init servises closed");
            Console.ReadLine();
            UpdateDrawingTimer.Stop();
        }

        private static void LocalDb_DataUpdated(object sender, EventArgs e)
        {
            Console.WriteLine("Data updated...");
           
            Console.WriteLine("Timer update data started");
            UpdateDrawingTimer.Start();
        }

        private static void UpdateDrawingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            UpdateDrawingTimer.Stop();
            Console.WriteLine("Update data started");
            RepositoryServer.LocalDb.UpdateDataAsync();
        }
    }
}
