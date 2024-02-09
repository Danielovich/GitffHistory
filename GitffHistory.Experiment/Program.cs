using GitffHistory.Experiment.Domain;
using GitffHistory.Experiment.IO;
using GitffHistory.Experiment.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IReadFiles<Commit>, CommitsReader>();
builder.Services.AddTransient<IDiffFiles, CommitDiffer>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
