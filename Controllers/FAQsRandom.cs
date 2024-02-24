using FAQwebApp.Data;
using Microsoft.AspNetCore.Mvc;

// Kontroler obsługujący losowe pytania z bazy danych FAQ
public class FAQsRandom : Controller
{
    private readonly ApplicationDbContext _context;

    // Konstruktor kontrolera, wstrzykujący kontekst bazy danych
    public FAQsRandom(ApplicationDbContext context)
    {
        _context = context;
    }

    // Akcja Index, wyświetlająca listę wszystkich pytań FAQ
    public IActionResult Index()
    {
        // Pobranie wszystkich pytań FAQ z bazy danych
        var faqs = _context.FAQ.ToList();
        return View(faqs); // Zwrócenie widoku zawierającego listę pytań
    }

    // Akcja Details, wyświetlająca szczegóły wybranego pytania FAQ
    public IActionResult Details(int id)
    {
        // Pobranie pytania FAQ o podanym identyfikatorze
        var faq = _context.FAQ.FirstOrDefault(q => q.id == id);
        if (faq == null)
        {
            return NotFound(); // Jeśli pytanie nie zostało znalezione, zwróć NotFound
        }
        return View(faq); // Zwróć widok z szczegółami pytania
    }

    // Akcja RandomQuestion, przekierowująca do losowego pytania FAQ
    public IActionResult RandomQuestion()
    {
        // Losowe wybranie jednego pytania z bazy danych
        var randomQuestion = _context.FAQ.OrderBy(q => Guid.NewGuid()).FirstOrDefault();
        if (randomQuestion == null)
        {
            return NotFound(); // Jeśli nie ma żadnych pytań, zwróć NotFound
        }
        return RedirectToAction("Details", "FAQs", new { id = randomQuestion.id }); // Przekierowanie do szczegółów losowego pytania
    }
}
