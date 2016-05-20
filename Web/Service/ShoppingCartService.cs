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
        private readonly PriceCalculator _priceCalculator;
        public ShoppingCartService(ProductService productService, PriceCalculator priceCalculator)
        {
            this._productService = productService;
            this._priceCalculator = priceCalculator;
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
            var shoppingCart = GetShoppingCart();
            var productInCart = shoppingCart.SelectedProducts.Find(s => s.Product.Id.Equals(productId));
            if (productInCart == null)
            {
                var product = _productService.GetById(productId);
                var selectedProduct = new SelectedProduct
                {
                    Amount = amount,
                    Product = product,
                };
                _priceCalculator.CalculateSelectedProduct(selectedProduct);
                shoppingCart.SelectedProducts.Add(selectedProduct);
            }
            else
            {
                var selectedProduct = shoppingCart.SelectedProducts.Find(p => p.Product.Id.Equals(productId));
                // Update amount
                selectedProduct.Amount = amount;
                // Calculate price
                _priceCalculator.CalculateSelectedProduct(selectedProduct);
                // Add back to list
            }
            HttpContext.Current.Session["ShoppingCart"] = shoppingCart;
            return true;
            
        }

        public bool AddToShoppingCart(int productId, int amount)
        {
            try
            {
                var shoppingCart = GetShoppingCart();
                var productInCart = shoppingCart.SelectedProducts.Find(s => s.Product.Id.Equals(productId));
                if (productInCart == null)
                {
                    var product = _productService.GetById(productId);
                    var selectedProduct = new SelectedProduct
                    {
                        Amount = amount,
                        Product = product
                    };
                    _priceCalculator.CalculateSelectedProduct(selectedProduct);
                    shoppingCart.SelectedProducts.Add(selectedProduct);
                }
                else
                {
                    var selectedProduct = shoppingCart.SelectedProducts.Find(p => p.Product.Id.Equals(productId));
                    // Update amount
                    selectedProduct.Amount+= amount;
                    // Calculate price
                    _priceCalculator.CalculateSelectedProduct(selectedProduct);
                }
                HttpContext.Current.Session["ShoppingCart"] = shoppingCart;
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool RemoveProductFromShoppingCart(int productId)
        {
            try
            {
                var shoppingCart = GetShoppingCart();
                var productInCart = shoppingCart.SelectedProducts.Find(s => s.Product.Id.Equals(productId));
                if (productInCart == null)
                {
                    return false;
                }

                var selectedProduct = shoppingCart.SelectedProducts.Find(p => p.Product.Id.Equals(productId));
                // Update amount
                shoppingCart.SelectedProducts.Remove(selectedProduct);
                HttpContext.Current.Session["ShoppingCart"] = shoppingCart;
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}