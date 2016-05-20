using System.Web;
using Model.Dto;
using Model.Entity;

namespace Web.Service
{
    public class PriceCalculator
    {
        private const double WHOLESALE_DISCOUNT_RATE = 0.05;

        public void CalculateSelectedProduct(SelectedProduct selectedProduct)
        {
            var isWholesale = HttpContext.Current.User.IsInRole("Wholesale");
            if (isWholesale)
            {
                selectedProduct.Price = selectedProduct.Product.Price*(1 - WHOLESALE_DISCOUNT_RATE);
                selectedProduct.CalculatePrice = selectedProduct.Price*selectedProduct.Amount;
            }
            else
            {
                selectedProduct.Price = selectedProduct.Product.Price;
                selectedProduct.CalculatePrice = selectedProduct.Price * selectedProduct.Amount;
            }
           
        }
    }
}