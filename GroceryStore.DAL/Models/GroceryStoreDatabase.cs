using System;
using System.Collections.Generic;

namespace GroceryStore.DAL.Models
{
    public class GroceryStoreDatabase
    {
        public GroceryStoreDatabase()
        {
            Customers = new List<Customer>();
        }

        public IEnumerable<Customer> Customers { get; set; }
    }
}
