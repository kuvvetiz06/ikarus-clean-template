using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// 1) HttpClient tanýmý
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// 2) MVC
builder.Services
    .AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

string enUSCulture = "en-US";
string trTRCulture = "tr-TR";
builder.Services.Configure<RequestLocalizationOptions>(options =>
{

    var supportLang = new[]
    {

        new CultureInfo(trTRCulture),
        new CultureInfo(enUSCulture)
    };
    options.DefaultRequestCulture = new RequestCulture(trTRCulture);
    options.SupportedCultures = supportLang;
    options.SupportedUICultures = supportLang;


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.Run();
