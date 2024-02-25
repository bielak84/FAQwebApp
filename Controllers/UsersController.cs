// UsersController.cs
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// Kontroler obsługujący operacje na użytkownikach
public class UsersController : Controller
{
    // Menedżer użytkowników, umożliwiający operacje na użytkownikach
    private readonly UserManager<IdentityUser> _userManager;

    // Konstruktor kontrolera, wstrzykujący menedżer użytkowników przez Dependency Injection
    public UsersController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    // Akcja wyświetlająca listę użytkowników
    public IActionResult Index()
    {
        // Pobranie listy wszystkich użytkowników
        var users = _userManager.Users.ToList();

        // Mapowanie użytkowników na obiekty ViewModel
        var userViewModels = users.Select(user => new UserViewModel
        {
            User = user,
            IsEmailConfirmed = user.EmailConfirmed
        }).ToList();

        // Przekazanie obiektów ViewModel do widoku
        return View(userViewModels);
    }

    // ViewModel reprezentujący użytkownika
    public class UserViewModel
    {
        public IdentityUser User { get; set; }
        public bool IsEmailConfirmed { get; set; }
    }

    // Akcja umożliwiająca edycję danych użytkownika
    public async Task<IActionResult> Edit(string id)
    {
        // Sprawdzenie czy podane id użytkownika istnieje
        if (id == null)
        {
            return NotFound();
        }

        // Pobranie użytkownika o danym id
        var user = await _userManager.FindByIdAsync(id);

        // Sprawdzenie czy użytkownik istnieje
        if (user == null)
        {
            return NotFound();
        }

        // Przekazanie użytkownika do widoku edycji
        return View(user);
    }

    // Akcja obsługująca zatwierdzenie edycji danych użytkownika
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email")] IdentityUser user)
    {
        // Sprawdzenie czy podane id użytkownika istnieje
        if (id != user.Id)
        {
            return NotFound();
        }

        // Sprawdzenie poprawności modelu (walidacja)
        if (ModelState.IsValid)
        {
            // Aktualizacja danych użytkownika w bazie danych
            var result = await _userManager.UpdateAsync(user);

            // Sprawdzenie czy aktualizacja zakończyła się sukcesem
            if (result.Succeeded)
            {
                // Przekierowanie do listy użytkowników po udanej edycji
                return RedirectToAction(nameof(Index));
            }
        }

        // Przekazanie użytkownika z błędami walidacji do widoku
        return View(user);
    }

    // Akcja umożliwiająca usunięcie użytkownika
    public async Task<IActionResult> Delete(string id)
    {
        // Sprawdzenie czy podane id użytkownika istnieje
        if (id == null)
        {
            return NotFound();
        }

        // Pobranie użytkownika o danym id
        var user = await _userManager.FindByIdAsync(id);

        // Sprawdzenie czy użytkownik istnieje
        if (user == null)
        {
            return NotFound();
        }

        // Przekazanie użytkownika do widoku usuwania
        return View(user);
    }

    // Akcja obsługująca zatwierdzenie usunięcia użytkownika
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        // Pobranie użytkownika o danym id
        var user = await _userManager.FindByIdAsync(id);

        // Sprawdzenie czy użytkownik istnieje
        if (user != null)
        {
            // Usunięcie użytkownika z bazy danych
            var result = await _userManager.DeleteAsync(user);

            // Sprawdzenie czy usunięcie zakończyło się sukcesem
            if (result.Succeeded)
            {
                // Przekierowanie do listy użytkowników po udanym usunięciu
                return RedirectToAction(nameof(Index));
            }
        }

        // Przekazanie użytkownika do widoku w przypadku niepowodzenia usunięcia
        return View(user);
    }
}
