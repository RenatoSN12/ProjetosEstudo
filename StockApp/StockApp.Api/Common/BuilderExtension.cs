using StockApp.Domain;

namespace StockApp.Api.Common;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder
            .Configuration
            .GetConnectionString("DefaultConnection") ?? string.Empty;
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }
    
    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => options.AddPolicy(ApiConfiguration.CorsPolicyName,
            policy => policy
                .WithOrigins([Configuration.BackendUrl, Configuration.FrontendUrl])
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()));
    }
}