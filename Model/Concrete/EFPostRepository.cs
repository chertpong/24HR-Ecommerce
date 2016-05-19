using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Model.Entity;
using Model.Repository;

namespace Model.Concrete
{
    public class EFPostRepository : IPostRepository, IDisposable
    {
        private readonly EFDbContext _context;
        private bool _disposed;

        public EFPostRepository(EFDbContext context)
        {
            this._context = context;
            this._disposed = false;
        }

        public List<Post> GetAll()
        {
            return _context.Posts.ToList();
        }

        public Post GetById(int id)
        {
            return _context.Posts.First(p => p.Id.Equals(id));
        }

        public void Create(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void Update(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var post = _context.Posts.Find(id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public List<Post> Find(Expression<Func<Post, bool>> predicate)
        {
            return _context.Posts.Where(predicate.Compile()).ToList();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}