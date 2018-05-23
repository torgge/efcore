using System.Collections.Generic;

namespace EFCore.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}