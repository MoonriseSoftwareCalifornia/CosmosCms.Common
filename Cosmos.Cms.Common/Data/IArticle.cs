using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos.Cms.Common.Data
{
    public interface IArticle
    {
        Guid Id { get; set; }
        string Title { get; set; }
        int ArticleNumber { get; set; }
        int StatusCode { get; set; }
        string UrlPath { get; set; }
        int VersionNumber { get; set; }
        DateTimeOffset? Published { get; set; }
        DateTimeOffset? Expires { get; set; }
        string Content { get; set; }
        DateTimeOffset Updated { get; set; }
        string HeaderJavaScript { get; set; }
        string FooterJavaScript { get; set; }
        string RoleList { get; set; }
    }
}
