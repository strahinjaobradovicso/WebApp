var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();

builder.Services.AddHttpClient<ITimeEntryRepository, TimeEntryRepository>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["HttpClients:TimeEntries:BaseAdress"]!);
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TimeEntry}/{action=Table}/{id?}");

app.Run();
