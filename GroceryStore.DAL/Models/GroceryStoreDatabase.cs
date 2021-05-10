using System;
using System.Collections.Generic;

namespace GroceryStore.DAL.Models
{
    public class GroceryStoreDatabase
    {
        public IEnumerable<Customer> Customers { get; set; }
    }
}
