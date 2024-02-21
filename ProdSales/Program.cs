using FluentValidation.AspNetCore;
using Application.Validators;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthorization();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
       .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegistrationValidator>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
