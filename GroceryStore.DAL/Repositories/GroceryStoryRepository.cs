using GroceryStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.DAL.Repositories
{
    public class GroceryStoryRepository<T> where T: BaseEntity
    {
        public async Task<T> GetById(int id) 
        {
        }

        public async Task<IEnumerable<T>> GetAll()
        {

        }

        public async Task<int> Create(T entity)
        {

        }
        public async Task Update(T entity)
        {

        }

    }
}
