namespace FAQwebApp.Models
{
    // Klasa reprezentuj�ca model danych b��du
    public class ErrorViewModel
    {
        // W�a�ciwo�� przechowuj�ca identyfikator zapytania, kt�re spowodowa�o b��d
        public string? RequestId { get; set; }

        // W�a�ciwo�� zwracaj�ca true, je�eli identyfikator zapytania nie jest pusty
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
