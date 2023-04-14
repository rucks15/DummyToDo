using SQLite;
using System;
using Xamarin.Forms;

namespace DummyToDo.Models
{
    public class Item
    {

        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Expense { get; set; }
        public string Category { get; set; }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        //Foreign key
        

        //protected virtual Budget Budget { get; set; }
        public int Budget_id { get; set; }

        
    }
}