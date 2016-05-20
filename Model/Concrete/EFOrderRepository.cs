using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Repository;

namespace Model.Concrete
{
   public  class EFOrderRepository : IOrderRepository
    {
        private readonly EFDbContext _context;


        public EFOrderRepository(EFDbContext context)
        {
            this._context = context;

        }
        public void Create(Order o)
        {
            _context.Orders.Add(o);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

       

        public List<Order> Find(Predicate<Order> function)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return _context.Orders.First(p => p.Id.Equals(id));
        }

        public void Update(Order o)
        {
            _context.Entry(o).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
