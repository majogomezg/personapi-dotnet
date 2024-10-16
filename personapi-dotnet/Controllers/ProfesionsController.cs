using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Repositories;

namespace personapi_dotnet.Controllers
{
    public class ProfesionsController : Controller
    {
        private readonly IProfesionRepository _profesionRepository;
        private readonly IEstudioRepository _estudioRepository;

        public ProfesionsController(IProfesionRepository profesionRepository, IEstudioRepository estudioRepository)
        {
            _profesionRepository = profesionRepository;
            _estudioRepository = estudioRepository;
        }

        // GET: Profesions
        public async Task<IActionResult> Index()
        {
            return View(await _profesionRepository.GetAllProfesionsAsync());
        }

        // GET: Profesions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // GET: Profesions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profesions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Des")] Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                await _profesionRepository.AddProfesionAsync(profesion);
                return RedirectToAction(nameof(Index));
            }
            return View(profesion);
        }

        // GET: Profesions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }
            return View(profesion);
        }

        // POST: Profesions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Des")] Profesion profesion)
        {
            if (id != profesion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _profesionRepository.UpdateProfesionAsync(profesion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _profesionRepository.ProfesionExistsAsync(profesion.Id))
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
            return View(profesion);
        }

        // GET: Profesions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null)
            {
                return NotFound();
            }

            return View(profesion);
        }

        // POST: Profesions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);

            if (profesion == null)
            {
                return NotFound();
            }

            // Verificar si la profesión tiene estudios asociados
            var tieneEstudios = await _estudioRepository.HasEstudiosForProfesionAsync(id);
            if (tieneEstudios)
            {
                ModelState.AddModelError("", "No se puede eliminar la profesión porque tiene estudios asociados.");
                return View(profesion);
            }

            // Si no tiene relaciones, proceder con la eliminación
            await _profesionRepository.DeleteProfesionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
