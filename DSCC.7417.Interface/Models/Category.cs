using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DSCC._7417.Interface.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name cannot be empty")]
        [MinLength(2, ErrorMessage = "Product name should have at least 2 characters")]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
    }
}
