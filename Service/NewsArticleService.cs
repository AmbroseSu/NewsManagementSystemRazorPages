using BusinessObject;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository iNewsArticleRepository;

        public NewsArticleService()
        {
            iNewsArticleRepository = new NewsArticleRepository();
        }

        public void SaveNewsArticle(NewsArticle newsArticle)
        {
            iNewsArticleRepository.SaveNewsArticle(newsArticle);
        }

        public void UpdateNewsArticle(NewsArticle newsArticle)
        {
            iNewsArticleRepository.UpdateNewsArticle(newsArticle);
        }
        
        public void DeleteNewsArticle(NewsArticle newsArticle)
        {
            iNewsArticleRepository.DeleteNewsArticle(newsArticle);
        }

        public List<NewsArticle> GetNewsArticles()
        {
            return iNewsArticleRepository.GetNewsArticles();
        }

        public NewsArticle GetNewsArticleById(string id)
        {
            return iNewsArticleRepository.GetNewsArticleById(id);
        }
        public NewsArticle GetArticleWithTag(string newsArticleId)
        {
            return iNewsArticleRepository.GetArticleWithTag(newsArticleId);
        }
        public List<NewsArticle> GetNewsArticlesById(string id)
        {
            return iNewsArticleRepository.GetNewsArticlesById(id);
        }

        public List<NewsArticle> GetNewsArticlesbyTitle(string title)
        {
            return iNewsArticleRepository.GetNewsArticlesbyTitle(title);
        }
        public List<NewsArticle> GetNewsArticlesByCategory(short categoryId)
        {
            return iNewsArticleRepository.GetNewsArticlesByCategory(categoryId);
        }
        public List<NewsArticle> GetNewsArticlesByStatus(bool status)
        {
            return iNewsArticleRepository.GetNewsArticlesByStatus(status);
        }
        public List<NewsArticle> GetNewsArticlesByTag(string tag)
        {
            return iNewsArticleRepository.GetNewsArticlesByTag(tag);
        }

        public List<NewsArticle> GetNewsArticlesByCreateById(short id)
        {
            return iNewsArticleRepository.GetNewsArticlesByCreateById(id);
        }
        public List<NewsArticle> GetNewsArticlesByStartEndDay(DateTime? startDate, DateTime? endDate)
        {
            return iNewsArticleRepository.GetNewsArticlesByStartEndDay(startDate, endDate);
        }

        public List<int> GetTagsByNewsArticleId(string id)
        {
            return iNewsArticleRepository.GetTagsByNewsArticleId(id);
        }


    }
}
