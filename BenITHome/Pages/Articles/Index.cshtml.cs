using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BenITHome.Data;
using BenITHome.Models;

namespace BenITHome.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly BenITHomeContext _context;

        public IndexModel(BenITHomeContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var articles = from a in _context.Article select a;
            if (!string.IsNullOrEmpty(SearchString))
            {
                articles = articles.Where(s => s.Title.Contains(SearchString));
            }

            Article = await articles.ToListAsync();
        }

        // public async Task OnGetAsync()
        // {
        //     Article = await _context.Article.ToListAsync();
        // }
    }
}
