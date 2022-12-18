using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OMS.API.Filters;
using OMS.API.Middlewares;
using OMS.API.Modules;
using OMS.Common.Configurations.Caching;
using OMS.Data;
using OMS.Service.Mapping.Users;
using OMS.Service.Validations;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                "http://www.contoso.com")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// Add services to the container.
#pragma warning disable CS0618 // Type or member is obsolete
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserLoginDtoValidator>());
#pragma warning restore CS0618 // Type or member is obsolete
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;

});

builder.Services.AddControllers();

builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(i =>
    {
        i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        i.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        i.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            // set ClockSkew to zero so tokens expire exactly at token expiration time 
            ClockSkew = TimeSpan.Zero
        };
        options.SaveToken = true;
        options.Events = new JwtBearerEvents();
        options.Events.OnMessageReceived = context =>
        {

            if (context.Request.Cookies.ContainsKey("X-Access-Token"))
            {
                context.Token = context.Request.Cookies["X-Access-Token"];
            }

            return Task.CompletedTask;
        };
    })
    .AddCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.IsEssential = true;
    });









builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(NotFoundIdFilter<>));
builder.Services.AddScoped(typeof(NotFoundUpdateFilter<>));
builder.Services.AddAutoMapper(typeof(UserProfile));

builder.Services.AddDbContext<DatabaseContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(DatabaseContext))?.GetName().Name);
    });
});

builder.Services.AddOptions();

builder.Services.Configure<MemoryCacheConfig>(builder.Configuration.GetSection("MemoryCacheConfig"));

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new CoreModule()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DisplayRequestDuration();
        // .... other configurations
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("");

app.UseCustomException();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
