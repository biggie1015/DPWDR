using DPWDR.Technical.Interview.Data.Context;
using DPWDR.Technical.Interview.Data.Repositories;
using DPWDR.Technical.Interview.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Configuración de tu contexto de base de datos
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<SchedulerService>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
