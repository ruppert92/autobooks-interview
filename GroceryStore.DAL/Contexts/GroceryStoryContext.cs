using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using System.Dynamic;

namespace GroceryStore.DAL.Contexts
{
    public class GroceryStoryContext
    {
        private readonly string _databaseFilePath;
        private async Task<IEnumerable<T>> GetTable<T>(CancellationToken cancellationToken = default)
        {
            var databaseText = await File.ReadAllTextAsync(_databaseFilePath, cancellationToken);
            var database = JsonConvert.DeserializeObject<ExpandoObject>(databaseText);
            var table = (IEnumerable<T>)database.GetType().GetProperty(typeof(T).Name).GetValue(database);
            return table;
        }
    }
}
