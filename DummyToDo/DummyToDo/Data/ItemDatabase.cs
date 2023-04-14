using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using DummyToDo.Models;
using System.Threading.Tasks;

namespace DummyToDo.Data
{
    public class ItemDatabase
    {
        readonly SQLiteAsyncConnection connection;

        //Constructor
        public ItemDatabase(string dbpath)
        {
            connection = new SQLiteAsyncConnection(dbpath);
            connection.CreateTableAsync<Item>().Wait();
        }

        public Task<List<Item>> GetItemsAsync(int BudgetId)
        {
            return connection
                .Table<Item>()
                .Where (i => i.Budget_id == BudgetId)
                .ToListAsync();
        }

        public Task<Item> GetItemAsync(int id)
        {
            return connection.Table<Item>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Item item)
        {
            if (item.Id != 0)
            {
                //update an existing item
                return connection.UpdateAsync(item);
            }

            else
            {
                //save a new item
                return connection.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Item item)
        {
            //Deletes a record
            return connection.DeleteAsync(item);
        }
    }
}
