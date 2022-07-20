using System.Text.Json.Serialization;
using FreeHost.API.Extensions;
using FreeHost.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.SetConfigurations(builder.Configuration);
builder.Services.SetMapper();
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();
builder.Services.SetIdentity();
builder.Services.SetAuthentication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
