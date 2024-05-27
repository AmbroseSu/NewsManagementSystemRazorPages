using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCategories = new List<Category>();
            try
            {
                using var context = new FunewsManagementDbContext();
                listCategories = context.Categories.ToList();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCategories;
        }

        public static void SaveCategory(Category category)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.Categories.Add(category);
                context.SaveChanges();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateCategory(Category category)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                context.Entry<Category>(category).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public static void DeleteCategory(Category category)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                var ca = context.Categories.SingleOrDefault(cate => cate.CategoryId == category.CategoryId);
                context.Categories.Remove(ca);
                context.SaveChanges();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Category GetCategoryById(short id)
        {
            using var context = new FunewsManagementDbContext();
            return context.Categories.FirstOrDefault(cate => cate.CategoryId.Equals(id));
        }

        public static List<Category> GetCategoryByName(string name)
        {
            try
            {
                using var context = new FunewsManagementDbContext();
                return context.Categories
                              .Where(c => EF.Functions.Like(c.CategoryName, $"%{name}%"))
                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public static List<Category> GetCategoryByDescription(string description)
        {

            try
            {
                using var context = new FunewsManagementDbContext();
                return context.Categories
                              .Where(c => EF.Functions.Like(c.CategoryDesciption, $"%{description}%"))
                              .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }

    


}
