namespace GlucoseSensorDataPCL.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SQLite;
    using Models;
    using System.IO;

    public class BGRecordDatabase
    {
        readonly SQLiteAsyncConnection asyncConnection;
        readonly SQLiteConnection syncConnection;

        public BGRecordDatabase(string dbPath)
        {
            // Create the async connection and database if necessary
            //
            asyncConnection = new SQLiteAsyncConnection(dbPath);
            asyncConnection.CreateTableAsync<BGRecord>();

            // Also create the Synchronous connection for synchronous calls - readonly to avoid affecting any async writes
            //
            syncConnection = new SQLiteConnection(dbPath,SQLiteOpenFlags.ReadOnly);
        }

        public Task<List<BGRecord>> GetBGsAsync()
        {
            return asyncConnection.Table<BGRecord>().ToListAsync();
        }

        public Task<List<BGRecord>> GetLatestBGsAsync(int limit)
        {
            return asyncConnection.QueryAsync<BGRecord>(string.Format("SELECT * FROM [BGRecord] ORDER BY now DESC LIMIT {0}", limit));
        }

        public List<BGRecord> GetLatestBGsSync(int limit)
        {
            return syncConnection.Query<BGRecord>(string.Format("SELECT * FROM [BGRecord] ORDER BY now DESC LIMIT {0}", limit));
        }

        public Task<BGRecord> GetBGAsync(int id)
        {
            return asyncConnection.Table<BGRecord>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public BGRecord GetBGSync(int id)
        {
            return syncConnection.Table<BGRecord>().Where(i => i.ID == id).FirstOrDefault();
        }

        public Task<int> SaveBGAsync(BGRecord bgrecord)
        {
            if (bgrecord.ID != 0)
            {
                return asyncConnection.UpdateAsync(bgrecord);
            }
            else
            {
                return asyncConnection.InsertAsync(bgrecord);
            }
        }

        public Task<int> DeleteBGAsync(BGRecord bgrecord)
        {
            return asyncConnection.DeleteAsync(bgrecord);
        }

    }
}


