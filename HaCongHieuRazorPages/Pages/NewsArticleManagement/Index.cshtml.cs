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

        public async Task OnGetAsync()
        {
            var newsArticles = iNewsArticleService.GetNewsArticles();

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
