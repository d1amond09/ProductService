using ProductService.Infrastructure;
using ProductService.Application;
using ProductService.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddInfrastructure(builder.Configuration)
	.AddApplication()
	.AddPresentation(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
