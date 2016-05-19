using System;
using System.Linq;
using System.Web;
using Model.Dto;
using Model.Entity;
using Model.Service;

namespace Web.Service
{
    public class ShoppingCartService
    {
        private readonly ProductService _productService;

        public ShoppingCartService(ProductService productService)
        {
            this._productService = productService;
        }

        public ShoppingCart GetShoppingCart()
        {
            var shoppingCart = (ShoppingCart) HttpContext.Current.Session["ShoppingCart"];
            // New shopping cart if not exist
            shoppingCart = shoppingCart ?? new ShoppingCart();
            // Save current shopping cart to session
            HttpContext.Current.Session["ShoppingCart"] = shoppingCart;
            return shoppingCart;
        }

        public bool EmptyShoppingCart()
        {
            try
            {
                HttpContext.Current.Session["ShoppingCart"] = new ShoppingCart();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateShoppingCart(int productId, int amount)
        {
            try
            {
                var shoppingCart = GetShoppingCart();
                if (shoppingCart.SelectedProducts.First(p => p.Id.Equals(productId)) == null)
                {
                    var product = _productService.GetById(productId);
                    var selectedProduct = new SelectedProduct
                    {
                        Amount = amount,
                        Product = product,
                        CalculatePrice = product.Price*amount,
                        Price = product.Price
                    };
                    shoppingCart.SelectedProducts.Add(selectedProduct);
                }
                else
                {
                    shoppingCart.SelectedProducts.First(p => p.Id.Equals(productId)).Amount = amount;
                }
                HttpContext.Current.Session["ShoppingCart"] = shoppingCart;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}