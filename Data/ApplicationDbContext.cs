using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FAQwebApp.Models;

namespace FAQwebApp.Data
{
    // Klasa reprezentująca kontekst bazy danych, rozszerzająca domyślny kontekst IdentityDbContext
    public class ApplicationDbContext : IdentityDbContext
    {
        // Konstruktor przyjmujący opcje kontekstu bazy danych
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Właściwość DbSet reprezentująca tabelę FAQ w bazie danych
        public DbSet<FAQwebApp.Models.FAQ> FAQ { get; set; } = default!;
    }
}
