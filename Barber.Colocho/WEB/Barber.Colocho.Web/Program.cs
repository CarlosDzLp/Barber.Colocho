using Barber.Colocho.Core.Address;
using Barber.Colocho.Core.Auth;
using Barber.Colocho.Core.Company;
using Barber.Colocho.Core.Cron;
using Barber.Colocho.Core.Error;
using Barber.Colocho.Core.Helpers;
using Barber.Colocho.Core.INEGI;
using Barber.Colocho.Core.Plan;
using Barber.Colocho.Core.Service;
using Barber.Colocho.Core.User;
using Barber.Colocho.DataAccess.Db;
using Barber.Colocho.Repository.Repository;
using Barber.Colocho.Web.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
string connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionStrings), ServiceLifetime.Transient);

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(json => {
    json.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.OperationFilter<SwaggerHeaderParameterFilter>();
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese el token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
});
builder.Services.AddApiVersioning(
    options =>
    {
        options.ReportApiVersions = true;
    })
.AddApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

//REPOSITORY
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//REPOSITORY

//CLASS
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<AuthenticateBL>();
builder.Services.AddTransient<UserBL>();
builder.Services.AddTransient<InegiBL>();
builder.Services.AddTransient<AddressBL>();
builder.Services.AddTransient<CompanyBL>();
builder.Services.AddTransient<ServiceBL>();
builder.Services.AddTransient<PlanBL>();
builder.Services.AddTransient<ErrorBL>();
builder.Services.AddTransient<UploadImage>();
//CLASS


builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:JwtIssuer"],
            ValidAudience = builder.Configuration["Jwt:JwtAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:JwtKey"]))
        };
    });
builder.Services.AddResponseCaching();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCronJob<CronJobBL>("* * * * *");
var app = builder.Build();
app.Use(async (context, next) => 
{
    context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0"; // Para versiones HTTP/1.0
    await next();
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI(
    options =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            //options.SwaggerEndpoint($"/Colocho/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
