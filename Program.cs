using FAQwebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodawanie us³ug do kontenera us³ug (dependency injection).
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Dodawanie obs³ugi uwierzytelniania i autoryzacji
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Dodawanie obs³ugi kontrolerów i widoków
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Konfiguracja potoku przetwarzania ¿¹dañ HTTP
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint(); // Dodawanie obs³ugi koñcowej migracji bazy danych w trybie deweloperskim
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Obs³uga b³êdów w przypadku produkcji
    // Domyœlna wartoœæ HSTS wynosi 30 dni. Mo¿esz chcieæ zmieniæ to dla scenariuszy produkcyjnych, zobacz https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); // Dodawanie obs³ugi HSTS (HTTP Strict Transport Security)
}

app.UseHttpsRedirection(); // Przekierowanie ¿¹dañ HTTP na HTTPS
app.UseStaticFiles(); // Dodawanie obs³ugi plików statycznych (np. CSS, obrazy)

app.UseRouting(); // Dodawanie obs³ugi trasowania

app.UseAuthorization(); // Dodawanie obs³ugi autoryzacji

// Dodawanie trasowania dla akcji w kontrolerze FAQsRandom odpowiadaj¹cej za losowe pytanie
app.MapControllerRoute(
    name: "FAQsRandom",
    pattern: "FAQs/RandomQuestion",
    defaults: new { controller = "FAQsRandom", action = "RandomQuestion" });

// Domyœlne trasowanie dla kontrolerów i akcji
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Dodawanie obs³ugi stron Razor Pages

app.Run(); // Uruchomienie aplikacji
