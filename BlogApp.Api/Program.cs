using BlogApp.Data.Repositories.BlogRepository;
using BlogApp.Data;
using BlogApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Connect To Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IRepository<Blog>, Repository<Blog>>();

var app = builder.Build();

app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7286")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);

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
