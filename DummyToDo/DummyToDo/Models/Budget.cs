using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DummyToDo.Models
{
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Budget_Amount { get; set; }

        public decimal Balance { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
       
    }
}
