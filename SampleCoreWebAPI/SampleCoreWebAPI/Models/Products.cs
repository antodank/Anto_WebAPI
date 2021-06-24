using System;
using System.Collections.Generic;

namespace SampleCoreWebAPI.Models
{
    public partial class Products : BaseEntity
    {
        public int ID { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
