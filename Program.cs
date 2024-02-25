using FAQwebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodawanie usług do kontenera usług (dependency injection).
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Dodawanie obsługi uwierzytelniania i autoryzacji
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Dodawanie obsługi kontrolerów i widoków
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Konfiguracja potoku przetwarzania żądań HTTP
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Dodawanie obsługi końcowej migracji bazy danych w trybie deweloperskim
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Obsługa błędów w przypadku produkcji
    // Domyślna wartość HSTS wynosi 30 dni. Możesz chcieć zmienić to dla scenariuszy produkcyjnych, zobacz https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // Dodawanie obsługi HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Przekierowanie żądań HTTP na HTTPS
app.UseStaticFiles(); // Dodawanie obsługi plików statycznych (np. CSS, obrazy)

app.UseRouting(); // Dodawanie obsługi trasowania

app.UseAuthorization(); // Dodawanie obsługi autoryzacji

// Dodawanie trasowania dla akcji w kontrolerze FAQsRandom odpowiadającej za losowe pytanie
app.MapControllerRoute(
    name: "FAQsRandom",
    pattern: "FAQs/RandomQuestion",
    defaults: new { controller = "FAQsRandom", action = "RandomQuestion" });

// Program.cs
// ...

app.MapControllerRoute(
    name: "Users",
    pattern: "Users/{action=Index}/{id?}",
    defaults: new { controller = "Users" });

// ...


// Dodawanie trasowania do akcji ShowSearchSite
app.MapControllerRoute(
    name: "ShowSearchSite",
    pattern: "FAQs/ShowSearchSite",
    defaults: new { controller = "FAQ", action = "ShowSearchSite" });

// Dodawanie trasowania do akcji ShowSearchResults
app.MapControllerRoute(
    name: "ShowSearchResults",
    pattern: "FAQs/ShowSearchResults",
    defaults: new { controller = "FAQ", action = "ShowSearchResults" });

// Domyślne trasowanie dla kontrolerów i akcji
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Dodawanie obsługi stron Razor Pages

app.Run(); // Uruchomienie aplikacji
