using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MapApiApp.Model;
using SQLite;

namespace MapApiApp.Data
{
    public class MapData
    {
        readonly SQLiteAsyncConnection database;

        public MapData(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<MapCords>().Wait();
        }

        public Task<List<MapCords>> GetItemsAsync()
        {
            return database.Table<MapCords>().ToListAsync();
        }

        public Task<List<MapCords>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<MapCords>("SELECT * FROM [MapCords] WHERE [Done] = 0");
        }

        public Task<MapCords> GetItemAsync(int id)
        {
            return database.Table<MapCords>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(MapCords item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(MapCords item)
        {
            return database.DeleteAsync(item);
        }
    }
}
