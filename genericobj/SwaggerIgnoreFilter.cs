using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace AccountAndTransactions.genericobj
{
  
        public class SwaggerIgnoreFilter : ISchemaFilter
        {
            public void Apply(OpenApiSchema schema, SchemaFilterContext context)
            {
                if (schema?.Properties == null || context?.Type == null)
                {
                    return;
                }

                var excludedProperties = context.Type.GetProperties()
                    .Where(t => t.GetCustomAttributes(typeof(SwaggerIgnoreAttribute), true).Any())
                    .Select(t => t.Name);

                foreach (var prop in excludedProperties)
                {
                    if (schema.Properties.ContainsKey(prop))
                    {
                        schema.Properties.Remove(prop);
                    }
                }
            }
        }
    }

