using Mates.API.Middleware;
using Mates.API.StartupExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextService(builder.Configuration);
builder.Services.AddAuthenticationService();
builder.Services.AddFluentValidationService();
builder.Services.AddServices();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandlingMiddleware();

//app.UseInjectUserIdMiddleware();

app.MapControllers();

app.Run();
