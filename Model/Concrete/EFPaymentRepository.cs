using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            return _context.Payments.ToList();
        }

        public Payment GetById(int id)
        {
            return _context.Payments.Find(id);
        }

        public void Create(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public void Update(Payment payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var payment = _context.Payments.Find(id);
            _context.Payments.Remove(payment);
        }

        public List<Payment> Find(Expression<Func<Payment, bool>> predicate)
        {
            return _context.Payments.Where(predicate.Compile()).ToList();
        }
    }
}