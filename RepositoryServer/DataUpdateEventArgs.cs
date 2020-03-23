using System;


namespace RepositoryServer
{
    public static partial class LocalDb
    {
        public class DataUpdateEventArgs : EventArgs
        {
            public int Count { get; set; }
        }
    }
}
