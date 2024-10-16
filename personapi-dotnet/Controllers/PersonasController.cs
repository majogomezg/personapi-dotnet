using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Repositories;

namespace personapi_dotnet.Controllers
{
    public class PersonasController : Controller
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IEstudioRepository _estudioRepository;

        public PersonasController(IPersonaRepository personaRepository, ITelefonoRepository telefonoRepository, IEstudioRepository estudioRepository)
        {
            _personaRepository = personaRepository;
            _telefonoRepository = telefonoRepository;
            _estudioRepository = estudioRepository;
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            return View(await _personaRepository.GetAllPersonasAsync());
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cc,Nombre,Apellido,Genero,Edad")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                await _personaRepository.AddPersonaAsync(persona);
                return RedirectToAction(nameof(Index));
            }
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return View(persona);
        }

        // POST: Personas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cc,Nombre,Apellido,Genero,Edad")] Persona persona)
        {
            if (id != persona.Cc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _personaRepository.UpdatePersonaAsync(persona);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _personaRepository.PersonaExistsAsync(persona.Cc))
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
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = await _personaRepository.GetPersonaByIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            // Verificar si la persona tiene teléfonos asociados
            var tieneTelefonos = await _telefonoRepository.HasTelefonosAsync(id);
            if (tieneTelefonos)
            {
                ModelState.AddModelError("", "No se puede eliminar la persona porque tiene teléfonos asociados.");
                return View(persona);
            }

            // Verificar si la persona tiene estudios (profesiones) asociados
            var tieneEstudios = await _estudioRepository.HasEstudiosAsync(id);
            if (tieneEstudios)
            {
                ModelState.AddModelError("", "No se puede eliminar la persona porque tiene estudios asociados.");
                return View(persona);
            }

            // Si no tiene relaciones, proceder con la eliminación
            await _personaRepository.DeletePersonaAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
