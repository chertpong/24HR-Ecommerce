using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Model.Repository;

namespace Model.Service
{
   public class TagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            this._tagRepository = tagRepository;
        }


        public List<Tag> SearchTagsbyName(string tagName)
        {
            return _tagRepository.GetAll().Where(p => p.Name.Contains(tagName)).ToList<Tag>();
        }
        public List<Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }
        public Tag GetById(int id)
        {
            return _tagRepository.GetById(id);
        }

        public void Create(Tag t)
        {
            _tagRepository.Create(t);
        }

        public void Update(Tag t)
        {
            _tagRepository.Update(t);
        }

        public void Delete(int id)
        {
            _tagRepository.Delete(id);
        }
    }
}
