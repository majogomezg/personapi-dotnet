using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Repositories;

namespace personapi_dotnet.Controllers
{
    public class EstudiosController : Controller
    {
        private readonly IEstudioRepository _estudioRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;

        public EstudiosController(IEstudioRepository estudioRepository, IPersonaRepository personaRepository, IProfesionRepository profesionRepository)
        {
            _estudioRepository = estudioRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
        }

        // GET: Estudios
        public async Task<IActionResult> Index()
        {
            return View(await _estudioRepository.GetAllEstudiosAsync());
        }

        // GET: Estudios/Details
        public async Task<IActionResult> Details(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAsync(idProf, ccPer);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // GET: Estudios/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CcPer"] = new SelectList(
                (await _personaRepository.GetAllPersonasAsync()).Select(p => new
                {
                    Cc = p.Cc,
                    NombreCompleto = $"{p.Cc} - {p.Nombre} {p.Apellido}"
                }), "Cc", "NombreCompleto");

            ViewData["IdProf"] = new SelectList(
                (await _profesionRepository.GetAllProfesionsAsync()).Select(p => new
                {
                    Id = p.Id,
                    NombreProfesion = $"{p.Id} - {p.Nom}"
                }), "Id", "NombreProfesion");

            return View();
        }

        // POST: Estudios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                await _estudioRepository.AddEstudioAsync(estudio);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CcPer"] = new SelectList(
                (await _personaRepository.GetAllPersonasAsync()).Select(p => new
                {
                    Cc = p.Cc,
                    NombreCompleto = $"{p.Cc} - {p.Nombre} {p.Apellido}"
                }), "Cc", "NombreCompleto", estudio.CcPer);

            ViewData["IdProf"] = new SelectList(
                (await _profesionRepository.GetAllProfesionsAsync()).Select(p => new
                {
                    Id = p.Id,
                    NombreProfesion = $"{p.Id} - {p.Nom}"
                }), "Id", "NombreProfesion", estudio.IdProf);

            return View(estudio);
        }

        // GET: Estudios/Edit
        public async Task<IActionResult> Edit(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAsync(idProf, ccPer);
            if (estudio == null)
            {
                return NotFound();
            }

            ViewData["CcPer"] = new SelectList(
                (await _personaRepository.GetAllPersonasAsync()).Select(p => new
                {
                    Cc = p.Cc,
                    NombreCompleto = $"{p.Cc} - {p.Nombre} {p.Apellido}"
                }), "Cc", "NombreCompleto", estudio.CcPer);

            ViewData["IdProf"] = new SelectList(
                (await _profesionRepository.GetAllProfesionsAsync()).Select(p => new
                {
                    Id = p.Id,
                    NombreProfesion = $"{p.Id} - {p.Nom}"
                }), "Id", "NombreProfesion", estudio.IdProf);

            return View(estudio);
        }

        // POST: Estudios/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, int ccPer, [Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _estudioRepository.UpdateEstudioAsync(estudio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _estudioRepository.EstudioExistsAsync(estudio.IdProf, estudio.CcPer))
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

            ViewData["CcPer"] = new SelectList(
                (await _personaRepository.GetAllPersonasAsync()).Select(p => new
                {
                    Cc = p.Cc,
                    NombreCompleto = $"{p.Cc} - {p.Nombre} {p.Apellido}"
                }), "Cc", "NombreCompleto", estudio.CcPer);

            ViewData["IdProf"] = new SelectList(
                (await _profesionRepository.GetAllProfesionsAsync()).Select(p => new
                {
                    Id = p.Id,
                    NombreProfesion = $"{p.Id} - {p.Nom}"
                }), "Id", "NombreProfesion", estudio.IdProf);

            return View(estudio);
        }

        // GET: Estudios/Delete
        public async Task<IActionResult> Delete(int? idProf, int? ccPer)
        {
            if (idProf == null || ccPer == null)
            {
                return NotFound();
            }

            var estudio = await _estudioRepository.GetEstudioByIdAsync(idProf, ccPer);
            if (estudio == null)
            {
                return NotFound();
            }

            return View(estudio);
        }

        // POST: Estudios/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
        {
            await _estudioRepository.DeleteEstudioAsync(idProf, ccPer);
            return RedirectToAction(nameof(Index));
        }
    }
}
