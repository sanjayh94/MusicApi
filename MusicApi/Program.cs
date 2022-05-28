using MusicApi.Models;
using MusicApi.Services;
using Serilog;
using Serilog.Events;

namespace MusicApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region SerilogConfig
            // Configure Serilog Logging.
            // Serilog provides structured API event logs that can be easily extended to any supported log consuming platforms such as Splunk, Prometheus, Console, File, GrayLog, S3 and even Email!
            // https://github.com/serilog/serilog/wiki/Provided-Sinks
            // Setting up logging to Console for now
            builder.Host.UseSerilog((ctx, lc) => lc
                   .WriteTo.Console()); // Writing logs to Console 
            #endregion

            // Add services to the container.

            #region DBConfig
            // the configuration instance to which the appsettings.json file's MusicTracksDatabase section binds is registered in the Dependency Injection (DI) container. 
            builder.Services.Configure<MusicTracksDatabaseSettings>(
                builder.Configuration.GetSection("MusicTracksDatabase"));

            // The singleton service lifetime is most appropriate because TracksService takes a direct dependency on MongoClient.
            // Per the official Mongo Client reuse guidelines, MongoClient should be registered in DI with a singleton service lifetime.
            builder.Services.AddSingleton<TracksService>();
            #endregion

            builder.Services.AddControllers();
            // Register ASP.Net Core Healthcheck Middleware for container liveness checks. Basic healthchecks not tied to any subsystems. Access at '/health'
            builder.Services.AddHealthChecks();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();
            app.MapHealthChecks("/health"); //Map Healthcheck route

            app.Run();
        }
    }
}