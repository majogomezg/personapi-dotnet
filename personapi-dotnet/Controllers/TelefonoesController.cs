using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Repositories;

namespace personapi_dotnet.Controllers
{
    public class TelefonoesController : Controller
    {
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IPersonaRepository _personaRepository;

        public TelefonoesController(ITelefonoRepository telefonoRepository, IPersonaRepository personaRepository)
        {
            _telefonoRepository = telefonoRepository;
            _personaRepository = personaRepository;
        }

        // GET: Telefonoes
        public async Task<IActionResult> Index()
        {
            return View(await _telefonoRepository.GetAllTelefonosAsync());
        }

        // GET: Telefonoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // GET: Telefonoes/Create
        public async Task<IActionResult> Create()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewData["Duenio"] = new SelectList(personas.Select(p => new
            {
                Cc = p.Cc,
                NombreCompleto = $"{p.Nombre} {p.Apellido}"
            }), "Cc", "NombreCompleto");
            return View();
        }

        // POST: Telefonoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Num,Oper,Duenio")] Telefono telefono)
        {
            Console.WriteLine($"Is Valid {ModelState.IsValid}");

            if (!ModelState.IsValid)
            {
                // Obtener los errores
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                await _telefonoRepository.AddTelefonoAsync(telefono);
                return RedirectToAction(nameof(Index));
            }

            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewData["Duenio"] = new SelectList(personas.Select(p => new
            {
                Cc = p.Cc,
                NombreCompleto = $"{p.Nombre} {p.Apellido}"
            }), "Cc", "NombreCompleto", telefono.Duenio);
            return View(telefono);
        }

        // GET: Telefonoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewData["Duenio"] = new SelectList(personas.Select(p => new
            {
                Cc = p.Cc,
                NombreCompleto = $"{p.Nombre} {p.Apellido}"
            }), "Cc", "NombreCompleto", telefono.Duenio);
            return View(telefono);
        }

        // POST: Telefonoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Num,Oper,Duenio")] Telefono telefono)
        {
            if (id != telefono.Num)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _telefonoRepository.UpdateTelefonoAsync(telefono);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _telefonoRepository.TelefonoExistsAsync(telefono.Num))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewData["Duenio"] = new SelectList(personas.Select(p => new
            {
                Cc = p.Cc,
                NombreCompleto = $"{p.Nombre} {p.Apellido}"
            }), "Cc", "NombreCompleto", telefono.Duenio);
            return View(telefono);
        }

        // GET: Telefonoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            return View(telefono);
        }

        // POST: Telefonoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _telefonoRepository.DeleteTelefonoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
