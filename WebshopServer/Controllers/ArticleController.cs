using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Dtos;
using WebshopServer.Exceptions;
using WebshopServer.Interfaces;
using WebshopServer.QueryParameters;

namespace WebshopServer.Controllers
{
    [Route("api/articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public IActionResult GetAllArticles([FromQuery] ArticleQueryParameters queryParameters)
        {
            return Ok(_articleService.GetAllArticles(queryParameters));
        }

        [HttpGet("{id}")]
        public IActionResult GetArticleById(long id)
        {
            ArticleResponseDto article;

            try
            {
                article = _articleService.GetArticleById(id);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return Ok(article);
        }

        [HttpPost]
        [Authorize(Roles = "Seller", Policy = "IsVerifiedSeller")]
        public IActionResult CreateArticle([FromBody] ArticleRequestDto requestDto)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            ArticleResponseDto article;

            try
            {
                article = _articleService.CreateArticle(requestDto, userId);
            }
            catch (InvalidFieldsException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(article);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Seller", Policy = "IsVerifiedSeller")]
        public IActionResult UpdateArticle(long id, [FromBody] ArticleRequestDto requestDto)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            ArticleResponseDto article;

            try
            {
                article = _articleService.UpdateArticle(id, requestDto, userId);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidFieldsException e)
            {
                return BadRequest(e.Message);
            }
            catch (ForbiddenActionException)
            {
                return Forbid();
            }

            return Ok(article);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Seller", Policy = "IsVerifiedSeller")]
        public IActionResult DeleteArticle(long id)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            DeleteResponseDto responseDto;

            try
            {
                responseDto = _articleService.DeleteArticle(id, userId);
            }
            catch (ResourceNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (ForbiddenActionException)
            {
                return Forbid();
            }

            return Ok(responseDto);
        }
    }
}
