﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using Service;

namespace HaCongHieuRazorPages.Pages.TagManagement
{
    public class IndexModel : PageModel
    {
        private readonly ITagService iTagService;

        public IndexModel()
        {
            iTagService = new TagService();
        }

        public IList<Tag> Tag { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchInput { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTag { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5; // 5 tags per page
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (string.IsNullOrEmpty(role) || role != "Staff")
            {
                return RedirectToPage("/NewsArticleManagement/Index");
            }

            var tags = iTagService.GetTags();
            var tagsQuery = tags.AsQueryable();
            if (!string.IsNullOrEmpty(SearchInput) && !string.IsNullOrEmpty(SearchTag))
            {
                
                tags = SearchTag switch
                {
                    
                    "TagId" => tags.Where(c => c.TagId.ToString().Contains(SearchInput)).ToList(),
                    "TagName" => iTagService.GetTagsByName(SearchInput),
                    "Note" => iTagService.GetTagsByNote(SearchInput),
                    _ => tags
                };
            }

            /*Tag = tags;*/
            TotalPages = (int)Math.Ceiling(tagsQuery.Count() / (double)PageSize);

            var currentPageString = Request.Query["currentPage"];
            if (!int.TryParse(currentPageString, out int currentPage))
            {
                currentPage = 1;
            }
            CurrentPage = Math.Clamp(currentPage, 1, TotalPages);

            Tag = tagsQuery.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            return Page();
        }

        public bool IsSelected(string tag)
        {
            return string.Equals(SearchTag, tag, StringComparison.OrdinalIgnoreCase);
        }
    }
}
