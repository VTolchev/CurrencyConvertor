using CurrencyConvertor.Conversion;
using NLog.Extensions.Logging;

namespace CurrencyConvertor.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog();
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var corsPolicyName = "AllowConvertorUI";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName,
                    builder => builder
                        .AllowAnyOrigin() //TODO Configure only WebUI app host for PROD environment
                        //for example .WithOrigins("http://prod.domain:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services.AddSingleton<IConvertorFactory, ConvertorFactory>();
            builder.Services.AddSingleton<INumberParser>(new NumberParser(","));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors(corsPolicyName);

            app.Run();
        }
    }
}
