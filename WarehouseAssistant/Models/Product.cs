using System.Collections.Generic;

namespace WarehouseAssistant.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}
