using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;

namespace Model.Repository
{
    public interface ITagRepository : IDisposable
    {
        List<Tag> GetAll();
        Tag GetById(int id);
        void Create(Tag tag);
        void Update(Tag tag);
        void Delete(int id);
    }
}
