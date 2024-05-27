using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TagRepository : ITagRepository
    {
        public void SaveTag(Tag tag) => TagDAO.SaveTag(tag);
        public void UpdateTag(Tag tag) => TagDAO.UpdateTag(tag);
        public void DeleteTag(Tag tag) => TagDAO.DeleteTag(tag);
        public List<Tag> GetTags() => TagDAO.GetTags();
        public Tag GetTagById(int id) => TagDAO.GetTagById(id);
        public List<Tag> GetTagsByName(string name) => TagDAO.GetTagByName(name);
        public List<Tag> GetTagsByNote(string note) => TagDAO.GetTagByNote(note);
    }
}
