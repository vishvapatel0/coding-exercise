using DesignPatternsShowcase.Common.Configuration;
using DesignPatternsShowcase.Common.Logging;
using DesignPatternsShowcase.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DesignPatternsShowcase;

/// <summary>
/// Main entry point for the Design Patterns Showcase application.
/// Demonstrates 6 different design patterns with production-quality implementation.
/// </summary>
public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var host = CreateHostBuilder(args).Build();
            
            using var scope = host.Services.CreateScope();
            var application = scope.ServiceProvider.GetRequiredService<IApplication>();
            
            await application.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog((context, configuration) => 
                configuration.ReadFrom.Configuration(context.Configuration))
            .ConfigureServices((context, services) =>
            {
                services.Configure<ApplicationSettings>(
                    context.Configuration.GetSection(nameof(ApplicationSettings)));
                
                services.AddSingleton<IApplication, Application>();
                services.RegisterPatternServices();
            });
}
