using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinServiceHosts
{
    public partial class Service1 : ServiceBase
    {
        private static ServiceHost DrawingArxiveServiceHost;
        private readonly RepositoryServer.Keeper log = RepositoryServer.Keeper.Instance(RepositoryServer.Keepers.Log);
        private static System.Timers.Timer UpdateDrawingTimer;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log.Save("WinServiceHosts", "Start service hosts",EventLogEntryType.Information);
            RepositoryServer.LocalDb.DataUpdated += LocalDb_DataUpdated;
            UpdateDrawingTimer = new System.Timers.Timer(360000.0);// 1 hour
            UpdateDrawingTimer.Elapsed += UpdateDrawingTimer_Elapsed;
            RepositoryServer.LocalDb.UpdateDataAsync();
            try
            {
                DrawingArxiveServiceHost = new ServiceHost(typeof(DrawingArxiveService.DrawingArxiveService));
                DrawingArxiveServiceHost.Open();
            }
            catch (Exception err)
            {
                log.Save("WinServiceHosts - DrawingArxiveService", err.Message);
            }
        }

        private void LocalDb_DataUpdated(object sender, EventArgs e)
        {          
            UpdateDrawingTimer.Start();
        }

        private void UpdateDrawingTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            UpdateDrawingTimer.Stop();            
            RepositoryServer.LocalDb.UpdateDataAsync();
        }

        protected override void OnStop()
        {
            if (UpdateDrawingTimer != null)
            {
                UpdateDrawingTimer.Stop();
            }
        }
    }
}
