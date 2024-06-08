using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class TagService : ITagService
    {
        private readonly ITagRepository iTagRepository;

        public TagService()
        {
            iTagRepository = new TagRepository();
        }

        public void SaveTag(Tag tag)
        {
            iTagRepository.SaveTag(tag);
        }

        public void DeleteTag(Tag tag)
        {
            iTagRepository.DeleteTag(tag);
        }

        public void UpdateTag(Tag tag)
        {
            iTagRepository.UpdateTag(tag);
        }

        public List<Tag> GetTags()
        {
            return iTagRepository.GetTags();
        }
        public Tag GetTagById(int id)
        {
            return iTagRepository.GetTagById(id);
        }

        public List<Tag> GetTagsByName(string name)
        {
            return iTagRepository.GetTagsByName(name);
        }

        public List<Tag> GetTagsByNote(string note)
        {
            return iTagRepository.GetTagsByNote(note);
        }
        public List<Tag> GetTagsByIds(List<int> tagIds)
        {
            return iTagRepository.GetTagsByIds(tagIds);
        }

    }
}
