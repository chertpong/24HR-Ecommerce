using System;
using Model.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Model.Repository
{
    public interface IProductRepository : IDisposable
    {
        List<Product> GetAll();
        Product GetById(int id);
        void Create(Product product);
        void Update(Product product);
        void Delete(int id);

    }
}
