using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;


namespace Model.Repository
{
    public interface IOrderRepository : IDisposable
    {
        List<Order> GetAll();
        Order GetById(int id);
        void Create(Order o);
        void Update(Order o);
        void Delete(int id);
        List<Order> Find(Predicate<Order> function );
    }
}
