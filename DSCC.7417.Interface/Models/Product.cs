using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSCC._7417.Interface.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name cannot be empty")]
        [MinLength(2, ErrorMessage = "Product name should have at least 2 characters")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price cannot be empty")]
        [Range(0, int.MaxValue, ErrorMessage = "Price cannot be negative value")]
        public string Price { get; set; }
    }
}
