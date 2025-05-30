using suaconsulta_api.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using suaconsulta_api.Validator;
using FluentValidation;
using suaconsulta_api.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


var conexao = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<AppDbContext>(b => b.UseNpgsql(conexao));

builder.Services.AddScoped<IValidator<CreateConsultation>, CreateConsultationValidator>();
builder.Services.AddScoped<IValidator<CreateDoctorScheduleDto>, CreateDoctorScheduleValidator>();
builder.Services.AddScoped<IValidator<UpdateConsultation>, UpdateConsultationValidator>();

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateConsultationValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdateConsultationValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<CreateDoctorScheduleValidator>();
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

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
