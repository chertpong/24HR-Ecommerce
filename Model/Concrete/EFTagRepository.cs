using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Repository;
using System.Data.Entity;

namespace Model.Concrete
{
    public class EFTagRepository : ITagRepository, IDisposable
    {
        private readonly EFDbContext _context;
        private bool _disposed;

        public EFTagRepository(EFDbContext context)
        {
            this._context = context;
            this._disposed = false;
        }

        public void Create(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var tag = _context.Tags.Find(id);
            _context.Tags.Remove(tag);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Tag> GetAll()
        {
            return _context.Tags.ToList();
        }

        public Tag GetById(int id)
        {
            return _context.Tags.First(p => p.Id.Equals(id));
        }

        public void Update(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
