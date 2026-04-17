using appointmentapi.Data;
using appointmentapi.Repositories.AppointmentRepositories;
using appointmentapi.Repositories.Auth;
using appointmentapi.Repositories.Interface.AppointmentInterface;
using appointmentapi.Repositories.Interface.AuthInterface;
using appointmentapi.Services.Auth;
using appointmentapi.Services.Interface;
using appointmentapi.Services.Specialty;
using appointmentapi.Services.TimeSlots;
using appointmentapi.Services.UnitSpecialty;
using appointmentapi.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Services Configuration

// Suporte a controllers
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Appointment API", Version = "v1" });

    // Configuração do JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token JWT: 'Bearer {token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

#endregion

#region Database Configuration

var connectionString = builder.Configuration.GetConnectionString("AppointmentConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

#region Dependency Injection

// Repositórios
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
builder.Services.AddScoped<IUnitScheduleRepository, UnitScheduleRepository>();

// Serviços de negócio
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<ITimeSlotService, TimeSlotService>();



// Serviço de autenticação (orquestrador)
builder.Services.AddScoped<AuthService>();

#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy
                .WithOrigins("http://127.0.0.1:5500")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

#region Middleware Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowLocalhost");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();

