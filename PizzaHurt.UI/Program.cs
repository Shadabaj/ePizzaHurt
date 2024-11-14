using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using PizzaHurt.Services;
using PizzaHurt.UI.FileHelper;
using PizzaHurt.UI.Helpers;
using Serilog;
using WebMarkupMin.AspNetCore8;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx,lc)=>lc.ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
builder.Services.AddControllersWithViews();
ConfigureDependency.RegisterService(builder.Services, builder.Configuration);
builder.Services.AddScoped<ActivityLogFilter>();
builder.Services.AddScoped<CustomExceptionFilter>();
builder.Services.AddScoped<IFileHelper,Filehelper>();

// Configure anti-forgery options
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN"; // Custom header name for the anti-forgery token
    options.SuppressXFrameOptionsHeader = false; // Optional: enable X-Frame-Options header
});

//Cookies based Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "PizzaHurtPersistentCookie";
        options.LoginPath = "/Account/Login";
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Account/UnAuthorized";
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = builder.Configuration.GetSection("GoogleAuthSetting:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("GoogleAuthSetting:ClientSecret").Value;
        options.CallbackPath = "/signin-google"; // Ensure this path matches your Google Console settings
        options.SaveTokens = true;

        options.Events.OnRemoteFailure = context =>
        {
            context.HandleResponse(); // This stops the default exception throwing
            context.Response.Redirect("/Account/Login"); // Redirect to Login page on failure
            return Task.CompletedTask;
        };
    });




builder.Services.AddWebMarkupMin(options =>
{
    options.AllowMinificationInDevelopmentEnvironment = true;
    options.AllowCompressionInDevelopmentEnvironment = true;
    options.DisablePoweredByHttpHeaders = true;

}).AddHtmlMinification(options =>
{
    options.MinificationSettings.RemoveRedundantAttributes = true;
    options.MinificationSettings.MinifyInlineJsCode = true;
    options.MinificationSettings.MinifyInlineCssCode = true;
    options.MinificationSettings.MinifyEmbeddedJsonData = true;
    options.MinificationSettings.MinifyEmbeddedCssCode = true;
}).AddHttpCompression();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
//app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = 60 * 60 * 24 * 7; //Secs*Mins*Hrs*Days
        ctx.Context.Response.Headers["cache-control"] ="public, max-age=" + durationInSeconds;
    }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseWebMarkupMin();


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller}/{action}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
