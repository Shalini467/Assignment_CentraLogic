using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.CosmosDB;
using EmployeeManagementSystem.Interfaces;
using EmployeeManagementSystem.ServiceFilters;
using EmployeeManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICosmosDBInterface, CosmosDBService>();
builder.Services.AddScoped<IEmployeeBasicDetailsInterface, EmployeeBasicDetailsService>();
builder.Services.AddScoped<IEmployeeAdditionalDetailsInterface, EmployeeAdditionalDetailsService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<BuildBasicFilter>();

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
