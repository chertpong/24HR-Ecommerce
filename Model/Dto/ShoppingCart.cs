using System.Collections.Generic;
using Model.Entity;

namespace Model.Dto
{
    public class ShoppingCart
    {
        public List<SelectedProduct> SelectedProducts { get; set; } 
    }
}