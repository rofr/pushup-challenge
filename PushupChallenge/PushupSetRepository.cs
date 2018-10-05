using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;
using System.Linq;

namespace PushupChallenge
{
    public class PushupSetRepository
    {
        private SQLiteAsyncConnection _sql;

        public PushupSetRepository()
        {
            var mainDir = FileSystem.AppDataDirectory;
            var path = Path.Combine(mainDir, "pushups.db3");
            _sql = new SQLiteAsyncConnection(path);
        }

        public Task Initialize()
        {
            return _sql.CreateTableAsync<PushupSet>();
        }

        public Task Save(int reps, DateTime when)
        {
            var set = new PushupSet { Repetitions = reps, When = when };
            return _sql.InsertAsync(set);
        }

        public async Task<int> GetTotalRepetitions()
        {
            var items = await _sql.Table<PushupSet>().ToListAsync();
            return items.Sum(p => p.Repetitions);
        }
    }
}
