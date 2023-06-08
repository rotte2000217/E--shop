using AutoMapper;
using EntityFramework.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.Exceptions;
using WebshopServer.Infrastructure;
using WebshopServer.Interfaces;
using WebshopServer.Models;
using WebshopServer.QueryParameters;

namespace WebshopServer.Services
{
    public class ArticleService : IArticleService
    {
        private readonly WebshopDbContext _dbContext;
        private readonly IMapper _mapper;

        public ArticleService(WebshopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ArticleDto> GetAllArticles(ArticleQueryParameters queryParameters)
        {
            List<Article> articles = new List<Article>();

            if (queryParameters.SellerId > 0)
            {
                articles = _dbContext.Articles.Where(x => x.SellerId == queryParameters.SellerId).ToList();
            }
            else
            {
                articles = _dbContext.Articles.ToList();
            }

            return _mapper.Map<List<ArticleDto>>(articles);
        }

        public ArticleDto GetArticleById(long id)
        {
            ArticleDto article = _mapper.Map<ArticleDto>(_dbContext.Articles.Find(id));

            if (article == null)
            {
                throw new ResourceNotFoundException("Article with specified id doesn't exist!");
            }

            return article;
        }

        public ArticleDto CreateArticle(ArticleDto articleDto, long userId)
        {
            Article article = _mapper.Map<Article>(articleDto);
            article.SellerId = userId;

            _dbContext.Articles.Add(article);

            try
            {
                _dbContext.SaveChanges();
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidFieldsException("One of more fields are missing!");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<ArticleDto>(article);
        }

        public ArticleDto UpdateArticle(long id, ArticleDto articleDto, long userId)
        {
            Article article = _dbContext.Articles.Find(id);

            if (article == null)
            {
                throw new ResourceNotFoundException("Article with specified id doesn't exist!");
            }

            if (article.SellerId != userId)
            {
                throw new ForbiddenActionException("Sellers can only modify their own articles!");
            }

            article.Name = articleDto.Name;
            article.Price = articleDto.Price;
            article.Quantity = articleDto.Quantity;
            article.Description = articleDto.Description;

            try
            {
                _dbContext.SaveChanges();
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidFieldsException("One of more fields are missing!");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<ArticleDto>(article);
        }

        public void DeleteArticle(long id, long userId)
        {
            Article article = _dbContext.Articles.Find(id);

            if (article == null)
            {
                throw new ResourceNotFoundException("Article with specified id doesn't exist!");
            }
            
            if (article.SellerId != userId)
            {
                throw new ForbiddenActionException("Sellers can only delete their own articles!");
            }

            _dbContext.Articles.Remove(article);
            _dbContext.SaveChanges();
        }
    }
}
