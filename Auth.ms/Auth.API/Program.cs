
using Auth.Domain.Models;
using Auth.Persistance.Context;
using Auth.Persistance.Repositories;
using Auth.Services.Kafka;
using Auth.Services.Services;

namespace Auth.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AuthDbContext>();
            builder.Services.AddScoped<IAuthRepository,AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.Configure<KafkaProducerConfig>(builder.Configuration.GetSection("KafkaProducerConfig"));
            builder.Services.AddSingleton<KafkaProducerService>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
