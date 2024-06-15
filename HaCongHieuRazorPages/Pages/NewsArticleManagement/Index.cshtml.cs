using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Repository;
using Service;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HaCongHieuRazorPages.Pages.NewsArticleManagement
{
    public class IndexModel : PageModel
    {
        private readonly INewsArticleService iNewsArticleService;

        public IndexModel()
        {
            iNewsArticleService = new NewsArticleService();
        }

        public IList<NewsArticle> NewsArticle { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchInput { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchNewsArticle { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [BindProperty(SupportsGet = true)] 
        public DateTime EndDate { get; set; } = DateTime.Now;

        public string ErrorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool CheckFind {  get; set; } = false;
        public string role {  get; set; }
        public async Task OnGetAsync()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            role = HttpContext.Session.GetString("UserRole");
            var newsArticles = iNewsArticleService.GetNewsArticles();

            if (!string.IsNullOrEmpty(email) && /*!role.Equals("Admin") && !role.Equals("Lecturer")*/role.Equals("Staff"))
            {
                newsArticles = newsArticles.Where(n => n.CreatedBy.AccountEmail == email).ToList();
            }
            else
            {
                newsArticles = newsArticles;
            }
            DateTime startDateTime = StartDate.Date;
            DateTime endDateTime = EndDate.Date.AddDays(1).AddTicks(-1);
            if (role == "Admin") 
            { 
                if(CheckFind == true)
                {
                    if (startDateTime > endDateTime)
                    {
                        ErrorMessage = "Start Date cannot be later than End Date.";
                        NewsArticle = newsArticles;
                        return;
                    }
                    else
                    {
                        newsArticles = newsArticles.Where(na => na.CreatedDate >= startDateTime && na.CreatedDate <= endDateTime).ToList();
                    }
                }
                else
                {
                    newsArticles = newsArticles;
                }
                    
                
                
            }
            
            

            if (!string.IsNullOrEmpty(SearchInput) && !string.IsNullOrEmpty(SearchNewsArticle))
            {
                newsArticles = SearchNewsArticle switch
                {
                    "NewsArticleId" => iNewsArticleService.GetNewsArticlesById(SearchInput),
                    "NewsTitle" => iNewsArticleService.GetNewsArticlesbyTitle(SearchInput),
                    "CategoryId" => iNewsArticleService.GetNewsArticlesByCategory(short.Parse(SearchInput)),
                    "NewsStatus" => iNewsArticleService.GetNewsArticlesByStatus(bool.Parse(SearchInput)),
                    "CreateById" => iNewsArticleService.GetNewsArticlesByCreateById(short.Parse(SearchInput)),
                    "Tag" => iNewsArticleService.GetNewsArticlesByTag(SearchInput),
                    _ => newsArticles
                };
            }

            NewsArticle = newsArticles;
        }

        public bool IsSelected(string category)
        {
            return string.Equals(SearchNewsArticle, category, StringComparison.OrdinalIgnoreCase);
        }
    }
}
