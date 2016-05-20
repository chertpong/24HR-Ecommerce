using System.Collections.Generic;
using Model.Entity;

namespace Model.Dto
{
    public class ShoppingCart
    {
        public ShoppingCart(List<SelectedProduct> selectedProducts)
        {
            SelectedProducts = selectedProducts;
        }

        public ShoppingCart()
        {
            SelectedProducts = new List<SelectedProduct>();
        }

        public List<SelectedProduct> SelectedProducts { get; set; } 
    }
}