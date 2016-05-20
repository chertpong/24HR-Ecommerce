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
       private readonly ISelectedProductRepository _selectedProductRepository;

       public OrderService(IOrderRepository orderRepository, ISelectedProductRepository selectedProductRepository)
       {
           _orderRepository = orderRepository;
           _selectedProductRepository = selectedProductRepository;
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

       public void MakeOrder(List<SelectedProduct> selectedProducts ,string username, TransportationType transportationType,Payment payment,string note)
       {

            var order = new Order
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Note = note,
                Payment = payment,
                Username = username,
                Status = OrderStatus.PENDING,
            };
            _orderRepository.Create(order);
           foreach (var s in selectedProducts)
           {
               s.OrderId = order.Id;
               s.ProductId = s.Product.Id;
               s.Product = null;
               _selectedProductRepository.Create(s);
           }
       }

    }
}
