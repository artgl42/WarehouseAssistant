using System.Collections.Generic;

namespace WarehouseAssistant.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Transaction> TransactionFrom { get; set; }
        public virtual ICollection<Transaction> TransactionIn { get; set; }
    }
}
