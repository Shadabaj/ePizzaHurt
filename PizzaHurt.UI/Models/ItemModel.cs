using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace PizzaHurt.UI.Models
{
    public class ItemModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Display(Name = "Upload New File")]
        public IFormFile? File { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please Enter UnitPrice")]
        public decimal UnitPrice { get; set; }
       
        public string ImageUrl { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Please Select Category")]
        public int CategoryId { get; set; }

        [DisplayName("ItemType")]
        [Required(ErrorMessage = "Please Select Item")]
        public int ItemTypeId { get; set; }

    }
}
