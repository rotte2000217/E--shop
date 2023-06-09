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
        List<ArticleResponseDto> GetAllArticles(ArticleQueryParameters queryParameters);
        ArticleResponseDto GetArticleById(long id);
        ArticleResponseDto CreateArticle(ArticleRequestDto requestDto, long userId);
        ArticleResponseDto UpdateArticle(long id, ArticleRequestDto requestDto, long userId);
        DeleteResponseDto DeleteArticle(long id, long userId);
    }
}
