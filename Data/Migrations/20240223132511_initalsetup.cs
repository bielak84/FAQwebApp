using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAQwebApp.Data.Migrations
{
    /// <summary>
    /// Klasa migracji początkowej odpowiedzialna za ustawienie tabeli FAQ.
    /// </summary>
    public partial class initalsetup : Migration
    {
        /// <summary>
        /// Metoda odpowiedzialna za wprowadzanie zmian podczas migracji w górę (Up migration).
        /// </summary>
        /// <param name="migrationBuilder">Builder do tworzenia i modyfikowania obiektów bazy danych.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Tworzenie tabeli FAQ z niezbędnymi kolumnami.
            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),  // Automatycznie narastająca kolumna identyfikatora.
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    // Ustawienie klucza głównego dla tabeli FAQ.
                    table.PrimaryKey("PK_FAQ", x => x.id);
                });
        }

        /// <summary>
        /// Metoda odpowiedzialna za cofanie zmian podczas migracji w dół (Down migration).
        /// </summary>
        /// <param name="migrationBuilder">Builder do tworzenia i modyfikowania obiektów bazy danych.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Usuwanie tabeli FAQ, jeśli migracja musi zostać cofnięta.
            migrationBuilder.DropTable(
                name: "FAQ");
        }
    }
}
