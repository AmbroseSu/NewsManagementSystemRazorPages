using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ITagService
    {
        void SaveTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(Tag tag);
        List<Tag> GetTags();
        Tag GetTagById(int id);
        List<Tag> GetTagsByName(string name);
        List<Tag> GetTagsByNote(string note);
    }
}
