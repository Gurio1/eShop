using Catalog.API;
using Catalog.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomSqlServer(builder.Configuration);

builder.Services.AddCustomDevCors();
builder.Services.AddCustomAutomaper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 
app.UseRequestExecutionTimeMiddleware();
    
app.UseAuthorization();

app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();
