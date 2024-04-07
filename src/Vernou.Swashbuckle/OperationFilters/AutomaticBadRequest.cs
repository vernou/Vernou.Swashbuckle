using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Vernou.Swashbuckle.OperationFilters
{
    /// <summary>
    /// The operation filter <see cref="AutomaticBadRequest"/> add a bad request response with the schema <see cref="ValidationProblemDetails"/>
    /// to operations generated from an controller action that had a parameter and can return
    /// <see href="https://learn.microsoft.com/en-us/aspnet/core/web-api#automatic-http-400-responses">a automatic bad request</see>.
    /// </summary>
    public sealed class AutomaticBadRequest : IOperationFilter
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomaticBadRequest"/> class.
        /// </summary>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> from <see cref="IHost"/>.</param>
        public AutomaticBadRequest(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if(HasParameter(operation) && HasModelStateInvalidFilter(context.ApiDescription.ActionDescriptor))
            {
                if(!operation.Responses.TryGetValue("400", out var response))
                {
                    response = new OpenApiResponse {
                        Description = "Bad Request"
                    };
                    operation.Responses.Add("400", response);
                }

                if(!response.Content.ContainsKey("application/json"))
                {
                    var schema = context.SchemaGenerator.GenerateSchema(typeof(ValidationProblemDetails), context.SchemaRepository);
                    response.Content.Add("application/json", new OpenApiMediaType {
                        Schema = schema
                    });
                }
            }
        }

        private bool HasParameter(OpenApiOperation operation)
        {
            return operation.Parameters.Any() || operation.RequestBody != null;
        }

        private bool HasModelStateInvalidFilter(ActionDescriptor actionDescriptor)
        {
            if(actionDescriptor is ControllerActionDescriptor des)
            {
                foreach(var fd in des.FilterDescriptors)
                {
                    if(fd.Filter is IFilterFactory ff)
                    {
                        var f = ff.CreateInstance(_serviceProvider);
                        if(f is ModelStateInvalidFilter)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
