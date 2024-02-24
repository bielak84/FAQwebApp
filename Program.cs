using FAQwebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodawanie us�ug do kontenera us�ug (dependency injection).
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Dodawanie obs�ugi uwierzytelniania i autoryzacji
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Dodawanie obs�ugi kontroler�w i widok�w
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Konfiguracja potoku przetwarzania ��da� HTTP
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Dodawanie obs�ugi ko�cowej migracji bazy danych w trybie deweloperskim
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Obs�uga b��d�w w przypadku produkcji
    // Domy�lna warto�� HSTS wynosi 30 dni. Mo�esz chcie� zmieni� to dla scenariuszy produkcyjnych, zobacz https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // Dodawanie obs�ugi HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Przekierowanie ��da� HTTP na HTTPS
app.UseStaticFiles(); // Dodawanie obs�ugi plik�w statycznych (np. CSS, obrazy)

app.UseRouting(); // Dodawanie obs�ugi trasowania

app.UseAuthorization(); // Dodawanie obs�ugi autoryzacji

// Dodawanie trasowania dla akcji w kontrolerze FAQsRandom odpowiadaj�cej za losowe pytanie
app.MapControllerRoute(
    name: "FAQsRandom",
    pattern: "FAQs/RandomQuestion",
    defaults: new { controller = "FAQsRandom", action = "RandomQuestion" });

// Domy�lne trasowanie dla kontroler�w i akcji
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Dodawanie obs�ugi stron Razor Pages

app.Run(); // Uruchomienie aplikacji
