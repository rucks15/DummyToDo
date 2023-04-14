using DummyToDo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DummyToDo.Data
{
    public class BudgetDatabase
    {
        readonly SQLiteAsyncConnection connection1;

        //Constructor
        public BudgetDatabase(string dbpath)
        {
            connection1 = new SQLiteAsyncConnection(dbpath);
            connection1.CreateTableAsync<Budget>().Wait();
        }

        public Task<List<Budget>> GetBudgetsAsync()
        {
            return connection1.Table<Budget>().ToListAsync();
        }

        public Task<Budget> GetBudgetAsync(int id)
        {
            return connection1.Table<Budget>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveBudgetAsync(Budget item)
        {
            if (item.Id != 0)
            {
                //update an existing item
                return connection1.UpdateAsync(item);
            }

            else
            {
                //save a new item
                return connection1.InsertAsync(item);
            }
        }
        public Task<int> DeleteBudgetAsync(Budget item)
        {
            //Deletes a record
            return connection1.DeleteAsync(item);
        }
    }
}
