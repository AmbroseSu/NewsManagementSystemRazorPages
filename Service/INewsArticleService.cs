using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface INewsArticleService
    {
        void SaveNewsArticle(NewsArticle newsArticle);
        void UpdateNewsArticle(NewsArticle newsArticle);
        void DeleteNewsArticle(NewsArticle newsArticle);
        List<NewsArticle> GetNewsArticles();
        NewsArticle GetNewsArticleById(string id);
        NewsArticle GetArticleWithTag(string newsArticleId);
        List<NewsArticle> GetNewsArticlesById(string id);
        List<NewsArticle> GetNewsArticlesbyTitle(string title);
        List<NewsArticle> GetNewsArticlesByCategory(short categoryId);
        List<NewsArticle> GetNewsArticlesByStatus(bool status);
        List<NewsArticle> GetNewsArticlesByTag(string tag);
        List<NewsArticle> GetNewsArticlesByCreateById(short id);
        List<NewsArticle> GetNewsArticlesByStartEndDay(DateTime? startDate, DateTime? endDate);
    }
}
