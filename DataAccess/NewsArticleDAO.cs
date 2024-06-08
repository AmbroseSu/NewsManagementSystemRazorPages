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
                                          .Include(ac => ac.CreatedBy)
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

                // Add the NewsArticle entity to the context
                context.NewsArticles.Add(newsArticle);

                // Ensure the state of each tag is unchanged to avoid re-inserting existing tags
                foreach (var tag in newsArticle.Tags)
                {
                    context.Entry(tag).State = EntityState.Unchanged;
                }

                // Save the NewsArticle entity to get its ID
                context.SaveChanges();

                // Avoid adding duplicate NewsTag entries
                var existingNewsTags = context.Set<Dictionary<string, object>>("NewsTag").AsNoTracking().Where(nt => nt["NewsArticleId"].ToString() == newsArticle.NewsArticleId).ToList();

                foreach (var tag in newsArticle.Tags)
                {
                    var tagId = tag.TagId;
                    if (!existingNewsTags.Any(nt => nt["TagId"].ToString() == tagId.ToString()))
                    {
                        context.Set<Dictionary<string, object>>("NewsTag").Add(new Dictionary<string, object>
                {
                    { "NewsArticleId", newsArticle.NewsArticleId },
                    { "TagId", tag.TagId }
                });
                    }
                }

                // Save changes to the join table
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static void UpdateNewsArticle(NewsArticle updatedNewsArticle)
        {
            try
            {
                using var context = new FunewsManagementDbContext();

                // Lấy bài viết hiện tại từ cơ sở dữ liệu cùng với các tag liên quan
                var existingNewsArticle = context.NewsArticles
                    .Include(n => n.Tags)
                    .FirstOrDefault(n => n.NewsArticleId == updatedNewsArticle.NewsArticleId);

                if (existingNewsArticle != null)
                {
                    // Cập nhật các thuộc tính của bài viết
                    existingNewsArticle.NewsTitle = updatedNewsArticle.NewsTitle;
                    existingNewsArticle.NewsContent = updatedNewsArticle.NewsContent;
                    existingNewsArticle.CategoryId = updatedNewsArticle.CategoryId;
                    existingNewsArticle.NewsStatus = updatedNewsArticle.NewsStatus;
                    existingNewsArticle.CreatedById = updatedNewsArticle.CreatedById;
                    existingNewsArticle.ModifiedDate = updatedNewsArticle.ModifiedDate;

                    // Xóa các tag hiện có
                    existingNewsArticle.Tags.Clear();

                    // Thêm các tag mới vào bài viết
                    foreach (var tag in updatedNewsArticle.Tags)
                    {
                        var tagEntity = context.Tags.Find(tag.TagId);
                        if (tagEntity != null)
                        {
                            context.Entry(tagEntity).State = EntityState.Unchanged;
                            existingNewsArticle.Tags.Add(tagEntity);
                        }
                    }

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("NewsArticle not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating NewsArticle: " + ex.Message);
            }
        }

        public static void DeleteNewsArticle(NewsArticle newsArticle)
        {
            try
            {
                using var context = new FunewsManagementDbContext();

                // Tìm bản ghi NewsArticle và bao gồm các liên kết với các Tag
                var near = context.NewsArticles
                                  .Include(na => na.Tags)
                                  .SingleOrDefault(na => na.NewsArticleId == newsArticle.NewsArticleId);

                if (near != null)
                {
                    // Xóa các liên kết trong bảng phụ NewsTag trước
                    foreach (var tag in near.Tags.ToList())
                    {
                        near.Tags.Remove(tag);
                    }

                    // Sau đó xóa NewsArticle
                    context.NewsArticles.Remove(near);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static NewsArticle GetNewsArticleById(string id)
        {
            using var context = new FunewsManagementDbContext();
            return context.NewsArticles.Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags).FirstOrDefault(na => na.NewsArticleId.Equals(id));
        }
        public static List<NewsArticle> GetNewsArticlesById(string id)
        {
            using var context = new FunewsManagementDbContext();
            return context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
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
                    .Include(n => n.CreatedBy)
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
                          .Include(cr => cr.CreatedBy)
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

        public static List<int> GetTagsByNewsArticleId(string id)
        {
                using var context = new FunewsManagementDbContext();
            var tagIds = context.NewsArticles
            .Where(n => n.NewsArticleId == id)
            .SelectMany(n => n.Tags.Select(t => t.TagId))
            .ToList(); ;
                return tagIds;
            
        }



    }
}
