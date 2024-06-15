using EmployeeManagementSystemDI.Entities;
using EmployeeManagementSystemDI.Interface;
using EmployeeManagementSystemDI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IEmployeeBasicDetailsService, EmployeeBasicDetailsService>();
builder.Services.AddScoped<IEmployeeAdditionalDetailsService, EmployeeAdditionalDetailsService>();

builder.Services.AddScoped<ICosmosDbService<EmployeeAdditionalDetails>, CosmosDbService<EmployeeAdditionalDetails>>();
builder.Services.AddScoped<ICosmosDbService<EmployeeBasicDetails>, CosmosDbService<EmployeeBasicDetails>>();

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
