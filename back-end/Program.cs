using System.Reflection;
using System.Text.Json.Serialization;
using back_end.Data;
using back_end.Middlewares;
using back_end.Repositories.MovementRepositories;
using back_end.Repositories.ProductRepositories;
using back_end.Repositories.UserRepositories;
using back_end.Services.MovementServices;
using back_end.Services.ProductServices;
using back_end.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace back_end;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Seu Projeto", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString"), npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure();
            }));
        builder.Services.AddAutoMapper(typeof(Program));
        
        // Registro dos Repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IMovementRepository, MovementRepository>();
        
        // Registro dos Services
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IMovementService, MovementService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        
        // Registra o middleware de tratamento de erros
        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.MapControllers();
        
        app.Run();
    }
}