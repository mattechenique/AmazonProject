using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description;
        public string? Price { get; set; }
        public int Quantity { get; set; }
        public bool IsBogo { get; set; }
        public decimal MarkDown { get; set; }
        public decimal TaxRate { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} - ${Price}\tAmount:{Quantity}\tDescription: {Description}";
        }
    }
}
