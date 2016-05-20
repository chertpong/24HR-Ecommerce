using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Model.Entity;
using Model.Repository;

namespace Model.Concrete
{
    public class EFPaymentRepository : IPaymentRepository
    {
        private readonly EFDbContext _context;

        public EFPaymentRepository(EFDbContext context)
        {
            _context = context;
        }

        public List<Payment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Payment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void Update(Payment payment)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Post> Find(Expression<Func<Payment, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}