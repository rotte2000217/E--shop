using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;

namespace WebshopServer.Interfaces
{
    public interface IArticleService
    {
        List<ArticleDto> GetAllArticles();
        ArticleDto GetArticleById(long id);
        ArticleDto CreateArticle(ArticleDto articleDto);
        ArticleDto UpdateArticle(long id, ArticleDto articleDto);
        void DeleteArticle(long id);
    }
}
