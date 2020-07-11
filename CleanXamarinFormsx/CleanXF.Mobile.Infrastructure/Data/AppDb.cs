using SQLite;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CleanXF.Mobile.Infrastructure.Data
{
    public class AppDb
    {
        public SQLiteConnection SyncDb { get; }
        public SQLiteAsyncConnection AsyncDb { get; }
        private readonly TimeSpan _busyTimeout = new TimeSpan(0, 0, 0, 5);

        public AppDb(string name, string path)
        {
            string dbPath = Path.Combine(path, name);

            if (!File.Exists(dbPath)) { LoadFromResources(name, dbPath); }

            if (SyncDb == null)
            {
                SyncDb = new SQLiteConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex)
                {
                    BusyTimeout = _busyTimeout
                };
            }

            if (AsyncDb != null)
            {
                return;
            }

            AsyncDb = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
            AsyncDb.SetBusyTimeoutAsync(_busyTimeout);
        }


        private static void LoadFromResources(string name, string path)
        {
            Type type = typeof(AppDb);
            Stream stream = type.GetTypeInfo().Assembly.GetManifestResourceStream(Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(n => n.Contains(name)));
            using (FileStream fileStream = File.Create(path))
            {
                if (stream == null)
                {
                    throw new NullReferenceException($"Failed to read local sqlite data: {path}");
                }

                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }
        }
    }
}
