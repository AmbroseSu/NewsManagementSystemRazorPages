using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ITagRepository
    {
        List<Tag> GetTags();
        Tag GetTagById(int id);
        void SaveTag(Tag tag);
        void DeleteTag(Tag tag);
        void UpdateTag(Tag tag);
        List<Tag> GetTagsByName(string name);
        List<Tag> GetTagsByNote(string note);
    }
}
