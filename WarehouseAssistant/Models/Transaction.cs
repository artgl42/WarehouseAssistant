using System;

namespace WarehouseAssistant.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int? WarehouseFromId { get; set; }
        public int? WarehouseInId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public DateTime DateTime { get; set; }
        public Warehouse WarehouseFrom { get; set; }
        public Warehouse WarehouseIn { get; set; }
        public Product Product { get; set; }
        
    }
}
