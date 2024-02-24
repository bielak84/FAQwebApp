using FAQwebApp.Data;
using Microsoft.AspNetCore.Mvc;

public class FAQsRandom : Controller
{
    private readonly ApplicationDbContext _context;

    public FAQsRandom(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var faqs = _context.FAQ.ToList();
        return View(faqs);
    }

    public IActionResult Details(int id)
    {
        var faq = _context.FAQ.FirstOrDefault(q => q.id == id);
        if (faq == null)
        {
            return NotFound();
        }
        return View(faq);
    }

    public IActionResult RandomQuestion()
    {
        var randomQuestion = _context.FAQ.OrderBy(q => Guid.NewGuid()).FirstOrDefault();
        if (randomQuestion == null)
        {
            return NotFound();
        }
        return RedirectToAction("Details", "FAQs", new { id = randomQuestion.id });
    }
}
