using GroceryStore.DAL.Attributes;

namespace GroceryStore.DAL.Models
{
    [TableName("customers")]
    public class Customer: BaseEntity
    {
        public string Name { get; set; }
    }
}
