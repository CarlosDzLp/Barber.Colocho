using Barber.Colocho.Transversal.Swagger.Configure;
using Barber.Colocho.Web.Configure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceConfigure(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.AddSwaggerConfigure(builder.Services, app);
app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
   );


await app.RunAsync();