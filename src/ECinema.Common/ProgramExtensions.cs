using ECinema.Common.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
        // if (!app.Environment.IsDevelopment())
        //     return;

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    public static void AddMongoDbSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
    }
}