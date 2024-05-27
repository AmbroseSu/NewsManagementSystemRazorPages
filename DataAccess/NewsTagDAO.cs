/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class NewsTagDAO
    {
        public static List<NewsArticle> GetNewsArticles(int tagId) 
        {
            using var context = new FunewsManagementDbContext();
            var articles = context.NewsTags
                               .Where(nt => nt.TagId == tagId)
                               .Include(nt => nt.NewsArticle)
                               .Select(nt => nt.NewsArticle)
                               .ToList();
            return articles;
        }

        public static List<Tag> GetTags(string newsArticleId)
        {
            using var context = new FunewsManagementDbContext();
            var tags = context.NewsTags
                           .Where(nt => nt.NewsArticleId == newsArticleId)
                           .Include(nt => nt.Tag)
                           .Select(nt => nt.Tag)
                           .ToList();
            return tags;
        }
        public static void SaveNewsTag(string newsArticleId, int tagId)
        {
            using var context = new FunewsManagementDbContext();

            var newsTag = new NewsTag
            {
                NewsArticleId = newsArticleId,
                TagId = tagId
            };

            context.NewsTags.Add(newsTag);
            context.SaveChanges();
        }

    }
}
*/