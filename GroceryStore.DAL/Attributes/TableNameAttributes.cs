using System;
namespace GroceryStore.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; set; }

        public TableNameAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
