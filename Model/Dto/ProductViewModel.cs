using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;

namespace Model.Dto
{
  public  class ProductViewModel
    {
        [Required]
         [DataType(DataType.Text)]
         [Display(Name = "Name")]
         public string Name { get; set; }
 
        [Required]
        [Display(Name = "Price")]
         public double Price { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        [Required]
         [Display(Name = "Amount")]
         public int Amount { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
