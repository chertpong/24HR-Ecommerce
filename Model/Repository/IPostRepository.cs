using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Model.Entity;

namespace Model.Repository
{
    public interface IPostRepository : IDisposable
    {
        List<Post> GetAll();
        Post GetById(int id);
        void Create(Post post);
        void Update(Post post);
        void Delete(int id);

        List<Post> Find(Expression<Func<Post, bool>> predicate);
    }
}