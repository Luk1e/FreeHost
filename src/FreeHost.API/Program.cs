using FreeHost.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.SetConfigurations(builder.Configuration);
builder.Services.SetMapper();
builder.Services.AddControllers();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();
builder.Services.SetIdentity();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
