using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Model.Entity;

namespace Model.Repository
{
    public interface IPaymentRepository
    {
        List<Payment> GetAll();
        Payment GetById(int id);
        void Create(Payment payment);
        void Update(Payment payment);
        void Delete(int id);

        List<Post> Find(Expression<Func<Payment, bool>> predicate);
    }
}