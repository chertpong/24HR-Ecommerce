using Model.Entity;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Service
{
    public class ProductService
    {

        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public List<Product> SearchProductbyName(string productName) {
            return _productRepository.GetAll().Where(p => p.Name.Contains(productName)).ToList<Product>();
        }

        public List<Product> SearchProductByTag(string productTag)
        {
            var productList = _productRepository.GetAll().Where(p => p.Tags.Contains(productTag)).ToList<Product>();
            return productList;
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }

        public void Create(Product p)
        {
            _productRepository.Create(p);
        }

        public void Update(Product p)
        {
            _productRepository.Update(p);
        }

        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }
    }
}