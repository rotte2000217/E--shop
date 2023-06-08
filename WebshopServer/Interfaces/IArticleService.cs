using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.QueryParameters;

namespace WebshopServer.Interfaces
{
    public interface IArticleService
    {
        List<ArticleDto> GetAllArticles(ArticleQueryParameters queryParameters);
        ArticleDto GetArticleById(long id);
        ArticleDto CreateArticle(ArticleDto articleDto, long userId);
        ArticleDto UpdateArticle(long id, ArticleDto articleDto, long userId);
        void DeleteArticle(long id, long userId);
    }
}
