using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TuiToot.Server.Api.Dtos.Request;

namespace TuiToot.Server.Api.Filter
{
    public class AddFileUploadSupportFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody != null &&
                operation.RequestBody.Content.ContainsKey("multipart/form-data"))
            {
                return;
            }

            var fileParameters = context.MethodInfo
                .GetParameters()
                .Where(p => p.ParameterType == typeof(OrderCreationRequest));

            if (fileParameters.Any())
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>
                            {
                                { "DeliveryAddressId", new OpenApiSchema { Type = "string" } },
                                { "Products", new OpenApiSchema
                                    {
                                        Type = "array",
                                        Items = new OpenApiSchema
                                        {
                                            Type = "object",
                                            Properties = new Dictionary<string, OpenApiSchema>
                                            {
                                                { "BagTypeId", new OpenApiSchema { Type = "string" } },
                                                { "Image", new OpenApiSchema { Type = "string", Format = "binary" } }
                                            }
                                        }
                                    }
                                }
                            }
                            }
                        }
                    }
                };
            }
        }
    }
}
