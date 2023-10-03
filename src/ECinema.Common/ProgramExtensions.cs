using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECinema.Common;

public static class ProgramExtensions
{
    public static void AddCommonSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void UseCommonSwagger(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) 
            return;
        
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}