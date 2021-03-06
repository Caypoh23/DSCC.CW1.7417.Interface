using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
        [DisplayName("Category")]
        public Category ProductCategory { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Price cannot be empty")]
        public decimal Price { get; set; }

        public SelectList Categories { get; set; }
    }
}
