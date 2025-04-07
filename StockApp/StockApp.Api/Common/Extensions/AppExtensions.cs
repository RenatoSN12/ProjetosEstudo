namespace StockApp.Api.Common.Extensions;

public static class AppExtensions
{
    public static void UseAppConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(ApiConfiguration.CorsPolicyName);
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}