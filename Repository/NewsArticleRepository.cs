using BusinessObject;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public void SaveNewsArticle(NewsArticle newsArticle) => NewsArticleDAO.SaveNewsArticle(newsArticle);
        public void UpdateNewsArticle(NewsArticle newsArticle) => NewsArticleDAO.UpdateNewsArticle(newsArticle);
        public void DeleteNewsArticle(NewsArticle newsArticle) => NewsArticleDAO.DeleteNewsArticle(newsArticle);
        public List<NewsArticle> GetNewsArticles() => NewsArticleDAO.GetNewsArticles();
        public NewsArticle GetNewsArticleById(string id) => NewsArticleDAO.GetNewsArticleById(id);
        public NewsArticle GetArticleWithTag(string newsArticleId) => NewsArticleDAO.GetArticleWithTag(newsArticleId);
        public List<NewsArticle> GetNewsArticlesById(string id) => NewsArticleDAO.GetNewsArticlesById(id);
        public List<NewsArticle> GetNewsArticlesbyTitle(string title) => NewsArticleDAO.GetNewsArticlesByTitle(title);
        public List<NewsArticle> GetNewsArticlesByCategory(short categoryId) => NewsArticleDAO.GetNewsArticlesByCategory(categoryId);
        public List<NewsArticle> GetNewsArticlesByStatus(bool status) => NewsArticleDAO.GetNewsArticlesByStatus(status);
        public List<NewsArticle> GetNewsArticlesByTag(string tag) => NewsArticleDAO.GetNewsArticlesByTag(tag);
        public List<NewsArticle> GetNewsArticlesByCreateById(short id) => NewsArticleDAO.GetNewsArticlesByCreateById(id);
        public List<NewsArticle> GetNewsArticlesByStartEndDay(DateTime? startDate, DateTime? endDate) => NewsArticleDAO.GetNewsArticlesByStartEndDay(startDate, endDate);
    }
}
