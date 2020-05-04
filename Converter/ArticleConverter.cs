using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HanuEdmsApi.Converter
{
    public class ArticleConverter : OneWayConverter<Models.Article, ViewModels.Article>
    {
        private static ViewModels.Article FromDatabase(Models.Article article)
        {
            return new ViewModels.Article()
            {
                Title = article.Title,
                Content = article.Content,
                LastUpdate = article.LastUpdated.Ticks
            };
        }

        public ArticleConverter() : base(FromDatabase) { }
    }
}
