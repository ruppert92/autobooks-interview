using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Threading;
using GroceryStore.DAL.Models;
using GroceryStore.DAL.Attributes;
using System.Reflection;

namespace GroceryStore.DAL.Contexts
{
    public class GroceryStoreFileDbContext: IGroceryStoreDbContext
    {
        private readonly string _databaseFilePath;

        public GroceryStoreFileDbContext(string databaseFilePath)
        {
            if(!File.Exists(databaseFilePath))
            {
                throw new Exception($"{databaseFilePath}: File does not exist");
            }
            _databaseFilePath = databaseFilePath;
        }

        public async Task<T> GetById<T>(int id, CancellationToken cancellationToken = default) where T: BaseEntity
        {
            var table = await GetTable<T>(cancellationToken);
            return table.FirstOrDefault(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll<T>(CancellationToken cancellationToken = default)
        {
            var table = await GetTable<T>(cancellationToken);
            return table;
        }

        public async Task<T> Add<T>(T entity, CancellationToken cancellationToken = default) where T: BaseEntity
        {
            var table = await GetTable<T>(cancellationToken);
            var tableList = table.ToList();
            var lastId = tableList.Max(e => e.Id) ?? 0;
            entity.Id = lastId + 1;
            tableList.Add(entity);
            await WriteTable(tableList, cancellationToken);
            return entity;
        }

        public async Task Update<T>(T entity, CancellationToken cancellationToken = default) where T: BaseEntity
        {
            var table = await GetTable<T>(cancellationToken);
            var tableList = table.ToList();
            var existingEntityIndex = tableList.FindIndex(e => e.Id == entity.Id);
            if(existingEntityIndex < 0)
            {
                return;
            }
            tableList[existingEntityIndex] = entity;
            await WriteTable(tableList);
        }

        private async Task<IEnumerable<T>> GetTable<T>(CancellationToken cancellationToken = default)
        {
            var tableName = ((TableNameAttribute)typeof(T).GetCustomAttributes(typeof(TableNameAttribute), true)?.FirstOrDefault())?.TableName;
            if(String.IsNullOrEmpty(tableName))
            {
                throw new Exception($"Could not find table name for class {typeof(T).Name}");
            }
            var database = await GetDatabase(cancellationToken);
            var table = (IEnumerable<T>)typeof(GroceryStoreDatabase).GetProperty(tableName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(database);
            return table;
        }

        private async Task WriteTable<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            var tableName = ((TableNameAttribute)typeof(T).GetCustomAttributes(typeof(TableNameAttribute), true)?.FirstOrDefault())?.TableName;
            if (String.IsNullOrEmpty(tableName))
            {
                throw new Exception($"Could not find table name for class {typeof(T).Name}");
            }
            var database = await GetDatabase(cancellationToken);
            typeof(GroceryStoreDatabase).GetProperty(tableName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(database, entities);
            await File.WriteAllTextAsync(_databaseFilePath, JsonConvert.SerializeObject(database), cancellationToken);
        }

        private async Task<GroceryStoreDatabase> GetDatabase(CancellationToken cancellationToken = default)
        {
            var databaseText = await File.ReadAllTextAsync(_databaseFilePath, cancellationToken);
            var database = JsonConvert.DeserializeObject<GroceryStoreDatabase>(databaseText);
            return database;
        }
    }
}
