using Application.Flows.Access.Commands;
using Application.Flows.Authentication.Commands;
using Application.Models.Access;
using Application.Models.Authentication;
using Application.Request;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMemoryCache()
                //
                .AddTransient<IRequestHandler<CreateTokenCommand, TokenObjectModel>, CreateTokenHandler>().AddTransient<IValidator<CreateTokenCommand>, CreateTokenValidator>()
                .AddTransient<IRequestHandler<AccessRequestCommand, AccessGrantObjectModel>, AccessRequestHandler>().AddTransient<IValidator<AccessRequestCommand>, AccessRequestValidator>()
                //
                ;

            return services;
        }
    }
}