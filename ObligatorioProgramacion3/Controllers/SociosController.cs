using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioProgramacion3.Datos;
using ObligatorioProgramacion3.Models;

namespace ObligatorioProgramacion3.Controllers
{
    public class SociosController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor que inyecta el contexto de la base de datos
        public SociosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción GET para listar todos los socios con filtros por tipo de socio y local asociado
        public async Task<IActionResult> Index(int? tipoSocioId, int? localAsociadoId)
        {
            IQueryable<Socio> sociosQuery = _context.Socios
                .Include(s => s.TipoSocio)
                .Include(s => s.LocalAsociado);

            if (tipoSocioId.HasValue && tipoSocioId.Value != 0)
            {
                sociosQuery = sociosQuery.Where(s => s.TipoSocioId == tipoSocioId);
            }

            if (localAsociadoId.HasValue && localAsociadoId.Value != 0)
            {
                sociosQuery = sociosQuery.Where(s => s.LocalAsociadoId == localAsociadoId);
            }

            var socios = await sociosQuery.ToListAsync();

            var tipoSocios = await _context.TipoSocios.ToListAsync();
            tipoSocios.Insert(0, new TipoSocio { Id = 0, Nombre = "Todos" });
            ViewBag.TipoSocioId = new SelectList(tipoSocios, "Id", "Nombre", tipoSocioId);

            var locales = await _context.Locales.ToListAsync();
            locales.Insert(0, new Local { Id = 0, Nombre = "Todos" });
            ViewBag.LocalAsociadoId = new SelectList(locales, "Id", "Nombre", localAsociadoId);

            return View(socios);
        }

        // Acción GET para redireccionar con los filtros seleccionados
        public IActionResult FilterByTipoSocio(int? tipoSocioId, int? localAsociadoId)
        {
            return RedirectToAction(nameof(Index), new { tipoSocioId, localAsociadoId });
        }

        // Acción GET para mostrar los detalles de un socio específico
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .Include(s => s.TipoSocio)
                .Include(s => s.LocalAsociado)
                .Include(s => s.RutinasSocios)
                .ThenInclude(rs => rs.Rutina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // Acción GET para mostrar el formulario de creación de un nuevo socio
        public IActionResult Create()
        {
            ViewData["TipoSocioId"] = new SelectList(_context.TipoSocios, "Id", "Nombre");
            ViewData["LocalAsociadoId"] = new SelectList(_context.Locales, "Id", "Nombre");
            return View();
        }

        // Acción POST para crear un nuevo socio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Id,Nombre,Apellido,Telefono,TipoSocioId,LocalAsociadoId")] Socio socio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoSocioId"] = new SelectList(_context.TipoSocios, "Id", "Nombre", socio.TipoSocioId);
            ViewData["LocalAsociadoId"] = new SelectList(_context.Locales, "Id", "Nombre", socio.LocalAsociadoId);
            return View(socio);
        }

        // Acción GET para mostrar el formulario de edición de un socio existente
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .Include(s => s.TipoSocio)
                .Include(s => s.LocalAsociado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socio == null)
            {
                return NotFound();
            }
            ViewData["TipoSocioId"] = new SelectList(_context.TipoSocios, "Id", "Nombre", socio.TipoSocioId);
            ViewData["LocalAsociadoId"] = new SelectList(_context.Locales, "Id", "Nombre", socio.LocalAsociadoId);
            return View(socio);
        }

        // Acción POST para editar un socio existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Email,Id,Nombre,Apellido,Telefono,TipoSocioId,LocalAsociadoId")] Socio socio)
        {
            if (id != socio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocioExists(socio.Id))
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
            ViewData["TipoSocioId"] = new SelectList(_context.TipoSocios, "Id", "Nombre", socio.TipoSocioId);
            ViewData["LocalAsociadoId"] = new SelectList(_context.Locales, "Id", "Nombre", socio.LocalAsociadoId);
            return View(socio);
        }

        // Acción GET para mostrar el formulario de eliminación de un socio
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socio = await _context.Socios
                .Include(s => s.TipoSocio)
                .Include(s => s.LocalAsociado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socio == null)
            {
                return NotFound();
            }

            return View(socio);
        }

        // Acción POST para confirmar la eliminación de un socio
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var socio = await _context.Socios.FindAsync(id);
            if (socio != null)
            {
                _context.Socios.Remove(socio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método privado para verificar si un socio existe
        private bool SocioExists(int id)
        {
            return _context.Socios.Any(e => e.Id == id);
        }

        // Acción GET para mostrar el formulario de asignación de rutina a un socio
        [HttpGet]
        public IActionResult AsignarRutina(int id)
        {
            var socio = _context.Socios.Find(id);
            if (socio == null)
            {
                return NotFound();
            }

            ViewBag.Rutinas = new SelectList(_context.Rutinas, "Id", "Nombre");
            return View(new RutinaSocio { IdSocio = id });
        }

        // Acción POST para asignar una rutina a un socio
        [HttpPost]
        public IActionResult AsignarRutina(RutinaSocio rutinaSocio)
        {
            var existeRutina = _context.RutinasSocios
                .Any(rs => rs.IdSocio == rutinaSocio.IdSocio && rs.IdRutina == rutinaSocio.IdRutina);

            if (existeRutina)
            {
                ModelState.AddModelError(string.Empty, "Esta rutina ya está asignada a este socio.");
            }

            if (ModelState.IsValid)
            {
                _context.RutinasSocios.Add(rutinaSocio);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = rutinaSocio.IdSocio });
            }

            ViewBag.Rutinas = new SelectList(_context.Rutinas, "Id", "Nombre");
            return View(rutinaSocio);
        }

        // Acción GET para mostrar el formulario de edición de calificación de una rutina asignada a un socio
        [HttpGet]
        public IActionResult EditarCalificacion(int idSocio, int idRutina)
        {
            var rutinaSocio = _context.RutinasSocios
                .Include(rs => rs.Rutina)
                .Include(rs => rs.Socio)
                .FirstOrDefault(rs => rs.IdSocio == idSocio && rs.IdRutina == idRutina);

            if (rutinaSocio == null)
            {
                return NotFound();
            }

            return View(rutinaSocio);
        }

        // Acción POST para editar la calificación de una rutina asignada a un socio
        [HttpPost]
        public IActionResult EditarCalificacion(RutinaSocio rutinaSocio)
        {
            if (ModelState.IsValid)
            {
                _context.Update(rutinaSocio);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = rutinaSocio.IdSocio });
            }

            return View(rutinaSocio);
        }

        // Acción POST para eliminar una rutina asignada a un socio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarRutina(int idSocio, int idRutina)
        {
            var rutinaSocio = await _context.RutinasSocios
                .FirstOrDefaultAsync(rs => rs.IdSocio == idSocio && rs.IdRutina == idRutina);
            if (rutinaSocio == null)
            {
                return NotFound();
            }

            _context.RutinasSocios.Remove(rutinaSocio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = idSocio });
        }
    }
}
