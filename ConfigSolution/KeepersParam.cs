using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32;

namespace ConfigSolution
{
   
    public enum Keepers
    // List of types of keepers for save you parameters
    {
        Reg,
        Log
    };
    public abstract class Keeper
        // Abstract class for access and works in keepers classes
    {
        protected const string NAME_BUILD = "ServerDrawingArxive"; //All keepers can to working to this name
        private static Keeper reg;
        private static Keeper log;
        public static  Keeper Instance(Keepers TypeKeeper) 
        {
            switch (TypeKeeper)
            {
                //every Kepers to be havent branch this switch
                case Keepers.Reg:
                    if(reg == null) { reg = new RegKeeper(); } //Singleton reg
                    return reg;
                case Keepers.Log:
                    if (log == null) { log = new LogKeeper(); } //Singleton log
                    return log;
                default:
                    return null;
            };
        }
        public abstract bool Save(string key, string value);
        public abstract string GetParam(string key);
    }
    internal class RegKeeper : Keeper
    {
        public override string GetParam(string key)
        {
            throw new NotImplementedException();
        }

        public override bool Save(string key, string value)
        {
            
                // Path for registry you can to take any for your project
                RegistryKey local = Registry.LocalMachine;
                RegistryKey software = local.OpenSubKey("SOFTWARE");
                RegistryKey writingKey = software.CreateSubKey(NAME_BUILD); //this name to be build
                writingKey.SetValue(key, value);
                writingKey.Close();

           
            return true;
        }
    }
    internal class LogKeeper : Keeper
    {
        private readonly EventLog _log;
        internal LogKeeper()
        {
            if (!EventLog.SourceExists(NAME_BUILD))
            //Name source log you can to change for your name a project
            {
                EventLog.CreateEventSource(NAME_BUILD, NAME_BUILD);
            }
            if (_log == null)
            {
                _log = new EventLog { Source = NAME_BUILD };
            }
        }
        public override string GetParam(string key)
        {
            throw new NotImplementedException();
        }

        public override bool Save(string key, string value)
        {
            Debug.WriteLine("****************");
           
            _log.WriteEntry(string.Format("{0}/n{1}", key, value), EventLogEntryType.Error);
            return true;
        }
    }
}
