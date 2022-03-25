using System.Diagnostics;
using Wedding.Data;
using Wedding.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// CSS is handled by Tailwind minification
builder.Services.AddWebOptimizer(pipeline =>
    pipeline.MinifyJsFiles("app.js"));

// Email
builder.Services.RegisterEmailSettings(builder.Configuration);

// Database
builder.Services.AddDbContextFactory<WeddingContext>();
await WeddingContext.Migrate();
await WeddingContext.Seed(builder.Environment, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseWebOptimizer();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/Shared/_Host");

// Rebuild CSS on change in Development
if (app.Environment.IsDevelopment())
{
    var process = new ProcessStartInfo()
    {
        FileName = "npm",
        Arguments = "run --silent css",
    };

    await Task.Run(() => Process.Start(process), app.Lifetime.ApplicationStopping).ConfigureAwait(false);
}

await app.RunAsync();
