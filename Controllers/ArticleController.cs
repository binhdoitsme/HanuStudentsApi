using HanuEdmsApi.Converter;
using HanuEdmsApi.EF;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HanuEdmsApi.Controllers
{
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly HanuEdmsContext Context;
        private readonly OneWayConverter<Models.Article, ViewModels.Article> Converter;

        public ArticleController(HanuEdmsContext context)
        {
            Context = context;
            Converter = new ArticleConverter();
        }

        [HttpGet("/articles")]
        public IEnumerable<ViewModels.Article> Index()
        {
            return Context.Article.ToList().Select(article => Converter.ForwardConverter(article)).ToList();
        }
    }
}