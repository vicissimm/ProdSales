using FluentValidation.AspNetCore;
using Application.Validators;
using Application;
using Infrastructure;
using Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthorization();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddControllers()
//       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddProductValidator>());

var app = builder.Build();

app.UseAccessTokenMiddleware();


app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();


app.MapControllers();

app.Run();  

