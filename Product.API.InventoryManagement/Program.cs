using Product.API.InventoryManagement.Application;
using Product.API.InventoryManagement.Infrastructure.Configuration;
using Product.API.InventoryManagement.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add DbContext
builder.Services.AddDbContext<InventoryDbContext>();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(CustomMap));

//Add Services
builder.Services.AddScoped<IInventoryRepo,InventoryRepo>();
builder.Services.AddScoped<CRUDService>();

builder.Services.AddControllers();
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

app.Run();
