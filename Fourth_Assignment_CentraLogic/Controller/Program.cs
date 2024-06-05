using VisitorSecurityClearanceSystem.CosmosDB;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVisitorService,VisitorService>();
builder.Services.AddScoped<IOfficeService,OfficeService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<ISecurityService,SecurityService>();
builder.Services.AddScoped<IUserInterface,UserService>();


builder.Services.AddScoped<IOfficeCosmosDBService, OfficeCosmosDBService>();
builder.Services.AddScoped<IManagerCosmosDBService, ManagerCosmosDBService>();
builder.Services.AddScoped<IVisitorCosmosDBService, VisitorCosmosDBService>();
builder.Services.AddScoped<ISecurityCosmosDBService, SecurityCosmosDBService>();

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
