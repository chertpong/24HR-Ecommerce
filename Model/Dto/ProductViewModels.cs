using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Entity;

namespace Model.Dto
{
    public class ProductViewModels
    {
        public class ProductViewModel
        {
            public int Id { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Price")]
            public double Price { get; set; }

            [Display(Name = "Description")]
            public string Description { get; set; }

            [Display(Name = "Thumbnail")]
            public string Thumbnail { get; set; }

            [Display(Name = "Amount")]
            public int Amount { get; set; }

            public List<Tag> Tags { get; set; }
        }
    }
}