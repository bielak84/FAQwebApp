//prop
//ctor
namespace FAQwebApp.Models
{
    // Klasa reprezentująca model FAQ 
    public class FAQ
    {
        // Identyfikator pytania
        public int id { get; set; }

        // Treść pytania
        public string Question { get; set; }

        // Treść odpowiedzi
        public string Answer { get; set; }

        // Konstruktor domyślny
        public FAQ()
        {
            // Pusta implementacja, ponieważ nie wymaga dodatkowej logiki w konstruktorze
        }
    }
}

