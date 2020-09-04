using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UpsideDownKitten.Common
{
    public class HideInDocsFilter : IDocumentFilter
    {
        public IConfiguration Configuration;

        public HideInDocsFilter(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var isBasicAuth = Configuration.GetValue<bool>("AppSettings:IsBasicAuth");
            if (isBasicAuth)
            {
                swaggerDoc.Paths.Remove("/api/Token");
            }
        }
    }
}
