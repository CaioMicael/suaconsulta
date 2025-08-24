using suaconsulta_api.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using suaconsulta_api.Validator;
using FluentValidation;
using suaconsulta_api.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using suaconsulta_api.Repositories;
using suaconsulta_api.middlewares;
using suaconsulta_api.Domain.Services;

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

//Filters
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResultFilter>();
});

// Classes base
builder.Services.AddScoped<RepositoryBase>();

// Demais classes
builder.Services.AddScoped<InterfaceAuthService, AuthService>();
builder.Services.AddScoped<InterfaceAuthRepository, AuthRepository>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<InterfaceUserRepository, userRepository>();
builder.Services.AddScoped<userRepository>();
builder.Services.AddScoped<PatientRepository>();
builder.Services.AddScoped<ConsultationRepository>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<DoctorRepository>();

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

var configuration = builder.Configuration;
var jwtKey = configuration["Jwt:Key"];
var jwtIssuer = configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Minha API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token JWT assim: Bearer {seu token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
