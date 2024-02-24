namespace FAQwebApp.Models
{
    // Klasa reprezentuj¹ca model danych b³êdu
    public class ErrorViewModel
    {
        // W³aœciwoœæ przechowuj¹ca identyfikator zapytania, które spowodowa³o b³¹d
        public string? RequestId { get; set; }

        // W³aœciwoœæ zwracaj¹ca true, je¿eli identyfikator zapytania nie jest pusty
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
