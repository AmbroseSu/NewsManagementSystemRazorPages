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
        private readonly ISystemAccountService iSystemAccountService;

        public IndexModel()
        {
            iNewsArticleService = new NewsArticleService();
            iSystemAccountService = new SystemAccountService();
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
        public bool CheckFind { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public bool CheckTrue { get; set; } = false;
        public int Count { get; set; }
        public string role {  get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // 5 tags per page
        public int TotalPages { get; set; }

        public async Task OnGetAsync()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            role = HttpContext.Session.GetString("UserRole");
            var newsArticles = iNewsArticleService.GetNewsArticles();
            var newsArticlesQuery = newsArticles.AsQueryable();

            if (!string.IsNullOrEmpty(email) && /*!role.Equals("Admin") && !role.Equals("Lecturer")*/role.Equals("Staff"))
            {
                SystemAccount systemAccount = iSystemAccountService.GetAccountByEmail(email);
                short id = systemAccount.AccountId;
                newsArticles = newsArticles.Where(n => n.CreatedById == id).ToList();
                newsArticlesQuery = newsArticles.AsQueryable();
            }
            else
            {
                newsArticles = newsArticles;
                newsArticlesQuery = newsArticles.AsQueryable();
            }
            DateTime startDateTime = StartDate.Date;
            DateTime endDateTime = EndDate.Date.AddDays(1).AddTicks(-1);
            var checkFindFromRequest = Request.Query["CheckFind"];
            //var startDateFromRequest = Request.Query["StartDate"];
            //var endDateFromRequest = Request.Query["EndDate"];
            /*if (role == "Admin") 
            {
                if(CheckTrue == true)
                {
                    TempData.Remove("CheckFind");
                    TempData.Remove("StartDate");
                    TempData.Remove("EndDate");
                }
                if (TempData.ContainsKey("CheckFind"))
                {
                    CheckFind = Convert.ToBoolean(TempData["CheckFind"]);
                   
                }
                if (CheckFind == true)
                {
                    
                    if (TempData.ContainsKey("StartDate") && TempData.ContainsKey("EndDate")) {
                        startDateTime = Convert.ToDateTime(TempData["StartDate"]);
                        endDateTime = Convert.ToDateTime(TempData["EndDate"]);
                    }
                    if (startDateTime > endDateTime)
                    {
                        ErrorMessage = "Start Date cannot be later than End Date.";
                        NewsArticle = newsArticles;
                        newsArticlesQuery = newsArticles.AsQueryable();
                        return;
                    }
                    else
                    {
                        newsArticles = newsArticles.Where(na => na.CreatedDate >= startDateTime && na.CreatedDate <= endDateTime).ToList();
                        Count = newsArticles.Count;
                        TempData["CheckFind"] = CheckFind;
                        TempData["StartDate"] = startDateTime;
                        TempData["EndDate"] = endDateTime;
                        newsArticlesQuery = newsArticles.AsQueryable();
                    }
                }*/

            if (role == "Admin")
            {
                if (TempData.ContainsKey("CheckTrue")) // "Reload" button clicked
                {
                    TempData.Remove("CheckFind");
                    TempData.Remove("StartDate");
                    TempData.Remove("EndDate");
                    TempData.Remove("CheckTrue");
                    CheckFind = false; // Reset CheckFind flag
                }
                if ((TempData.ContainsKey("CheckFind").Equals(true) && CheckTrue == false) || (CheckFind == true && CheckTrue == false))
                {
                    CheckFind = true;
                    var submittedStartDate = Request.Query["StartDate"];
                    var submittedEndDate = Request.Query["EndDate"];

                    // Retrieve start and end dates from TempData (if available)
                    if (!string.IsNullOrEmpty(submittedStartDate))
                    {
                        startDateTime = Convert.ToDateTime(submittedStartDate).ToLocalTime().Date; // Set to 00:00 of StartDate
                        TempData["StartDate"] = startDateTime.ToString("dd-MMM-yy");
                    }
                    else
                    {
                        if (TempData.ContainsKey("StartDate"))
                        {

                            startDateTime = Convert.ToDateTime(TempData["StartDate"]);


                        }
                    }
                    if (!string.IsNullOrEmpty(submittedEndDate))
                    {
                        endDateTime = Convert.ToDateTime(submittedEndDate).ToLocalTime().Date.AddHours(23).AddMinutes(59).AddSeconds(59); // Set to 23:59 of EndDate
                        TempData["EndDate"] = endDateTime.ToString("dd-MMM-yy");
                    }
                    else
                    {
                        if (TempData.ContainsKey("EndDate"))
                        {
                            endDateTime = Convert.ToDateTime(TempData["EndDate"]);

                        }
                    }


                    if (startDateTime > endDateTime)
                    {
                        ErrorMessage = "Start Date cannot be later than End Date.";
                        NewsArticle = newsArticles;
                        newsArticlesQuery = newsArticles.AsQueryable();
                        return;
                    }

                    newsArticles = newsArticles.Where(na => na.CreatedDate >= startDateTime && na.CreatedDate <= endDateTime).ToList();
                    Count = newsArticles.Count;
                    TempData["CheckFind"] = CheckFind; // Set TempData for next request
                    TempData["StartDate"] = startDateTime; // Set TempData for next request
                    TempData["EndDate"] = endDateTime; // Set TempData for next request
                    TempData["CheckTrue"] = true;
                    newsArticlesQuery = newsArticles.AsQueryable();
                }
                else
                {
                    CheckFind = false;
                    TempData.Remove("CheckFind"); // Remove TempData on "Reload"
                    TempData.Remove("StartDate");
                    TempData.Remove("EndDate");
                    TempData.Remove("CheckTrue");
                    newsArticles = newsArticles;
                    Count = newsArticles.Count;
                    newsArticlesQuery = newsArticles.AsQueryable();
                }
            }
            /*else
            {
                newsArticles = newsArticles;
                Count = newsArticles.Count;
                newsArticlesQuery = newsArticles.AsQueryable();
            
            }*/
            
            

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
                newsArticlesQuery = newsArticles.AsQueryable();
            }

            //NewsArticle = newsArticles;
            TotalPages = (int)Math.Ceiling(newsArticlesQuery.Count() / (double)PageSize);

            var currentPageString = Request.Query["currentPage"];
            if (!int.TryParse(currentPageString, out int currentPage))
            {
                currentPage = 1;
            }
            CurrentPage = Math.Clamp(currentPage, 1, TotalPages);

            NewsArticle = newsArticlesQuery.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        }

        public bool IsSelected(string category)
        {
            return string.Equals(SearchNewsArticle, category, StringComparison.OrdinalIgnoreCase);
        }
    }
}
