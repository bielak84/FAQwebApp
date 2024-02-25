using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FAQwebApp.Data;
using FAQwebApp.Models;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;

namespace FAQwebApp.Controllers
{
    // Kontroler obsługujący operacje związane z pytaniami i odpowiedziami (FAQ)
    public class FAQsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor kontrolera, który przyjmuje kontekst bazy danych
        public FAQsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FAQs
        // Akcja zwracająca widok zawierający listę wszystkich FAQ
        public async Task<IActionResult> Index()
        {
            return View(await _context.FAQ.ToListAsync());
        }

        // GET: FAQs/ShowSearchForm
        // Akcja zwracająca widok do wyświetlenia formularza wyszukiwania
        public IActionResult ShowSearchSite()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowSearchResults(string searchTerm)
        {
            // Tutaj dokonaj wyszukiwania w bazie danych
            var searchResults = _context.FAQ
                .Where(f => f.Question.Contains(searchTerm) || f.Answer.Contains(searchTerm))
                .ToList();

            ViewBag.SearchTerm = searchTerm; // Przekazanie wartości do ViewBag
            return View(searchResults); // Przekazanie wyników wyszukiwania do widoku
        }

        [Authorize]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Pobranie konkretnego FAQ o podanym identyfikatorze
            var fAQ = await _context.FAQ
                .FirstOrDefaultAsync(m => m.id == id);

            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // GET: FAQs/Create
        // Akcja zwracająca widok do tworzenia nowego FAQ
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FAQs/Create
        // Akcja obsługująca dodawanie nowego FAQ do bazy danych
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Question,Answer")] FAQ fAQ)
        {
            // Sprawdzenie poprawności modelu przed dodaniem do bazy danych
            if (ModelState.IsValid)
            {
                // Dodanie nowego FAQ do kontekstu bazy danych
                _context.Add(fAQ);

                // Zapisanie zmian w bazie danych
                await _context.SaveChangesAsync();

                // Przekierowanie do widoku listy FAQ po dodaniu nowego elementu
                return RedirectToAction(nameof(Index));
            }

            // Jeśli model nie jest poprawny, zwróć widok z błędami
            return View(fAQ);
        }

        // GET: FAQs/Edit/5
        // Akcja zwracająca widok do edycji konkretnego FAQ
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Pobranie konkretnego FAQ o podanym identyfikatorze
            var fAQ = await _context.FAQ.FindAsync(id);

            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // POST: FAQs/Edit/5
        // Akcja obsługująca edycję konkretnego FAQ w bazie danych
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Question,Answer")] FAQ fAQ)
        {
            // Sprawdzenie, czy identyfikator FAQ w ścieżce URL zgadza się z identyfikatorem FAQ w modelu
            if (id != fAQ.id)
            {
                return NotFound();
            }

            // Sprawdzenie poprawności modelu przed zapisaniem zmian w bazie danych
            if (ModelState.IsValid)
            {
                try
                {
                    // Aktualizacja FAQ w kontekście bazy danych
                    _context.Update(fAQ);

                    // Zapisanie zmian w bazie danych
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Obsługa błędów związanych z równoległym dostępem do danych
                    if (!FAQExists(fAQ.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Przekierowanie do widoku listy FAQ po zapisaniu zmian
                return RedirectToAction(nameof(Index));
            }

            // Jeśli model nie jest poprawny, zwróć widok z błędami
            return View(fAQ);
        }

        // GET: FAQs/Delete/5
        // Akcja zwracająca widok do potwierdzenia usunięcia konkretnego FAQ
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Pobranie konkretnego FAQ o podanym identyfikatorze
            var fAQ = await _context.FAQ
                .FirstOrDefaultAsync(m => m.id == id);

            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // POST: FAQs/Delete/5
        // Akcja obsługująca potwierdzenie usunięcia konkretnego FAQ z bazy danych
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Pobranie konkretnego FAQ o podanym identyfikatorze
            var fAQ = await _context.FAQ.FindAsync(id);

            if (fAQ != null)
            {
                // Usunięcie FAQ z kontekstu bazy danych
                _context.FAQ.Remove(fAQ);
            }

            // Zapisanie zmian w bazie danych
            await _context.SaveChangesAsync();

            // Przekierowanie do widoku listy FAQ po usunięciu
            return RedirectToAction(nameof(Index));
        }

        // Metoda sprawdzająca istnienie FAQ o określonym identyfikatorze
        private bool FAQExists(int id)
        {
            return _context.FAQ.Any(e => e.id == id);
        }
    }
}
