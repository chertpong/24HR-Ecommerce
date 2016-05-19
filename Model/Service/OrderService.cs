using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Repository;

namespace Model.Service
{
   public class OrderService
   {
       private readonly IOrderRepository _orderRepository;

       public OrderService(IOrderRepository orderRepository)
       {
           this._orderRepository = orderRepository;
       }

        public void Create(Order o)
        {
            _orderRepository.Create(o);
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }

        

        public List<Order> Find(Predicate<Order> function)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public void Update(Order o)
        {
            _orderRepository.Update(o);
        }

       public List<Order> GetAllFrom(DateTime startDate)
       {
            throw new NotImplementedException();
        }

       public List<Order> GetAllWithin(DateTime startDate, DateTime endDate)
       {
            throw new NotImplementedException();
        }

       public Order MakeOrder(List<SelectedProduct> selectedProducts ,string userId,TransportationType transportationType,Payment payment,string note)
       {
            throw new NotImplementedException();
        }

    }
}
