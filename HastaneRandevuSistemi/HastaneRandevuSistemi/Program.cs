using HastaneRandevuSistemi.Controllers;
using HastaneRandevuSistemi.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Localizer
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
options.DataAnnotationLocalizerProvider = (type, factory) =>
{
    var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
    return factory.Create(nameof(SharedResource), assemblyName.Name);
});

#endregion
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("tr-TR"),
    };

    options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
    options.SupportedCultures = supportCultures;
    options.SupportedUICultures = supportCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);                            // ekledim

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Kullanici}/{action=Index}/{id?}");

app.Run();
