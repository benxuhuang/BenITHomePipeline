
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using BenITHome.Models;
using System.Threading.Tasks;

namespace BenITHome.Data
{
    public class BenITHomeContext : DbContext
    {
        public BenITHomeContext(
            DbContextOptions<BenITHomeContext> options)
            : base(options)
        {
        }

        public DbSet<BenITHome.Models.Article> Article { get; set; }
   
           #region snippet1
        public async virtual Task<List<Article>> GetArticlesAsync()
        {
            return await Article
                .OrderBy(Article => Article.Title)
                .AsNoTracking()
                .ToListAsync();
        }
        #endregion

                #region snippet2
        public async virtual Task AddArticleAsync(Article m)
        {
            await Article.AddAsync(m);
            await SaveChangesAsync();
        }
        #endregion

                #region snippet3
        public async virtual Task DeleteAllArticlesAsync()
        {
            foreach (Article m in Article)
            {
                Article.Remove(m);
            }
            
            await SaveChangesAsync();
        }
        #endregion
   

        #region snippet4
        public async virtual Task DeleteArticleAsync(int id)
        {
            var m = await Article.FindAsync(id);

            if (m != null)
            {
                Article.Remove(m);
                await SaveChangesAsync();
            }
        }
        #endregion

                public void Initialize()
        {
            Article.AddRange(GetSeedingArticles());
            SaveChanges();
        }

            public static List<Article> GetSeedingArticles()
            {
                return new List<Article>()
                {
                    new Article(){ Title = "Day01 Azure 的自我修煉" },
                    new Article(){ Title = "Day02 申請Azure帳號" },
                    new Article(){ Title = "Day03 Resource Group 資源群組" }
                };
            }
    }
}