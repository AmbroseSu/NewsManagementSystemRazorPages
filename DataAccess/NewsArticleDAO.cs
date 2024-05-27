using Azure;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class NewsArticleDAO
    {
        public static List<NewsArticle> GetNewsArticles()
        {
            var listNewsArticles = new List<NewsArticle>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listNewsArticles = context.NewsArticles
                                          .Include(nt => nt.Tags)
                                          .Include(ca => ca.Category)
                                          .Where(nt => nt.NewsStatus == true)
                                          .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listNewsArticles;
        }

        /*public static List<Tag> GetNewsArticless()
        {
            var listNewsArticles = new List<NewsArticle>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listNewsArticles = context.NewsArticles.Include(c => c.Tags).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listNewsArticles;
        }*/

        public static void SaveNewsArticle(NewsArticle newsArticle)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.NewsArticles.Add(newsArticle);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateNewsArticle(NewsArticle newsArticle)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.Entry<NewsArticle>(newsArticle).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteNewsArticle(NewsArticle newsArticle)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                var near = context.NewsArticles.SingleOrDefault(na => na.NewsArticleId == newsArticle.NewsArticleId);
                context.NewsArticles.Remove(near);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static NewsArticle GetNewsArticleById(string id)
        {
            using var context = new FunewsManagementDbContext();
            return context.NewsArticles.FirstOrDefault(na => na.NewsArticleId.Equals(id));
        }
        public static List<NewsArticle> GetNewsArticlesById(string id)
        {
            using var context = new FunewsManagementDbContext();
            return context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.Tags)
                .Where(n => EF.Functions.Like(n.NewsArticleId, $"%{id}%"))
                    .ToList();
        }


        public static NewsArticle GetArticleWithTag(string newsArticleId)
        {
            using var context = new FunewsManagementDbContext();
            return context.NewsArticles.Include(ta => ta.Tags)
                .FirstOrDefault(ta => ta.NewsArticleId == newsArticleId);
        }

        public static List<NewsArticle> GetNewsArticlesByTitle(string title)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                return context.NewsArticles
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Where(n => EF.Functions.Like(n.NewsTitle, $"%{title}%"))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public static List<NewsArticle> GetNewsArticlesByCategory(short categoryId)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                return context.NewsArticles
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Where(n => n.CategoryId == categoryId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<NewsArticle> GetNewsArticlesByStatus(bool status)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                return context.NewsArticles
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Where(n => n.NewsStatus.Equals(status))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<NewsArticle> GetNewsArticlesByTag(string tag)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                return context.NewsArticles
                              .Include(na => na.Tags)
                              .Include(na => na.Category)
                              .Where(na => na.Tags.Any(t => EF.Functions.Like(t.TagName, $"%{tag}%")))
                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<NewsArticle> GetNewsArticlesByCreateById(short id)
        {
            using var context = new FunewsManagementDbContext();
            return context.NewsArticles
                          .Include(cr => cr.Category)
                          .Include(cr => cr.Tags)
                          .Where(cr => cr.CreatedById == id)
                          .ToList();
        }

        public static List<NewsArticle> GetNewsArticlesByStartEndDay(DateTime? startDate, DateTime? endDate)
        {
            using var context = new FunewsManagementDbContext();
            
            return context.NewsArticles
                          .Include(cr => cr.Category)
                          .Include(cr => cr.Tags)
                          .Where(cr => cr.CreatedDate >= startDate.Value && cr.CreatedDate <= endDate.Value)
                          .ToList();
        }



    }
}
