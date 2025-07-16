using Learning.Application.Common.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Learning.Infrastructure;
using Learning.Application.Locations.Queries;
using Mapster;
using Wolverine;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseWolverine(opts =>
{
    opts.ApplicationAssembly = typeof(GetAllLocationsQuery).Assembly;
});
var googleClientId = builder.Configuration["AuthClientId"];
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add Mapster configuration manually
TypeAdapterConfig.GlobalSettings.Scan(typeof(LocationMappingProfile).Assembly);
builder.Services.AddSingleton(TypeAdapterConfig.GlobalSettings);
builder.Services.AddScoped<IMapper, ServiceMapper>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Google Auth API", Version = "v1" });
    // Add OAuth2 flow using OpenID Connect (for ID token)
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://accounts.google.com/o/oauth2/v2/auth"),
                TokenUrl = new Uri("https://oauth2.googleapis.com/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "OpenID Connect scope" },
                    { "profile", "Profile info" },
                    { "email", "Email info" }
                }
            }
        },
        Description = "Google OAuth2 Login"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "oauth2",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new[] { "openid", "profile", "email" }
        }
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://accounts.google.com";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://accounts.google.com",

            ValidateAudience = true,
            ValidAudience = googleClientId,

            ValidateLifetime = true
        };
    }
    );
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Google Auth API V1");

    c.OAuthClientId(googleClientId);
    c.OAuthAppName("Swagger Google Auth POC");
    c.OAuthUsePkce(); // Required for Google (PKCE flow)
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
