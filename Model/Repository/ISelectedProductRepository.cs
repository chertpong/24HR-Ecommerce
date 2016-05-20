using System;
using System.Collections.Generic;
using Model.Entity;

namespace Model.Repository
{
    public interface ISelectedProductRepository
    {
        List<SelectedProduct> GetAll();
        SelectedProduct GetById(int id);
        void Create(SelectedProduct s);
        void Update(SelectedProduct s);
        void Delete(int id);
        List<SelectedProduct> Find(Predicate<SelectedProduct> function);
    }
}