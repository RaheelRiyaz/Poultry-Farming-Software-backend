using KashmirPoultrySoftware.Api;
using KashmirPoultrySoftware.Application;
using KashmirPoultrySoftware.Infrastructure;
using KashmirPoultrySoftware.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationService(builder.Environment.WebRootPath).
AddApiServices(builder.Configuration).
AddPersistenceServices(builder.Configuration).
AddInfraStructureServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(option =>
{
    option.SetIsOriginAllowed(_ => true)
    .AllowAnyHeader()
    .AllowAnyMethod();
});
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
