using suaconsulta_api.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using suaconsulta_api.Validator;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conexao = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<AppDbContext>(b => b.UseNpgsql(conexao));

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateConsultationValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdateConsultationValidator>();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
