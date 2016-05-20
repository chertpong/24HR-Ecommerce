using System;
using System.Collections.Generic;
using Model.Entity;
using Model.Repository;

namespace Model.Concrete
{
    public class EFSelectedProductRepository : ISelectedProductRepository
    {
        private readonly EFDbContext _context;

        public EFSelectedProductRepository(EFDbContext context)
        {
            _context = context;
        }

        public List<SelectedProduct> GetAll()
        {
            throw new NotImplementedException();
        }

        public SelectedProduct GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(SelectedProduct s)
        {
            _context.SelectedProducts.Add(s);
            _context.SaveChanges();
        }

        public void Update(SelectedProduct s)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<SelectedProduct> Find(Predicate<SelectedProduct> function)
        {
            throw new NotImplementedException();
        }
    }
}