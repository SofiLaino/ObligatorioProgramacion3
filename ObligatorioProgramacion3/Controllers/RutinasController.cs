using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObligatorioProgramacion3.Datos;
using ObligatorioProgramacion3.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

public class RutinasController : Controller
{
    private readonly ApplicationDbContext _context;

    // Constructor que inyecta el contexto de la base de datos
    public RutinasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Acción GET para listar todas las rutinas con filtrado por fecha
    public async Task<IActionResult> Index(DateTime? fechaInicio, DateTime? fechaFin, bool deshacer = false)
    {
        var rutinas = _context.Rutinas
            .Include(r => r.TipoRutina)
            .Include(r => r.RutinasSocios)
            .AsQueryable();

        if (!deshacer && fechaInicio.HasValue && fechaFin.HasValue)
        {
            rutinas = rutinas
                .Select(r => new Rutina
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    TipoRutina = r.TipoRutina,
                    RutinasSocios = r.RutinasSocios,
                    PromedioCalif = r.RutinasSocios
                        .Where(rs => rs.Fecha >= fechaInicio.Value && rs.Fecha <= fechaFin.Value)
                        .Average(rs => (decimal?)rs.Calificacion)
                });
        }
        else
        {
            rutinas = rutinas
                .Select(r => new Rutina
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    TipoRutina = r.TipoRutina,
                    RutinasSocios = r.RutinasSocios,
                    PromedioCalif = r.RutinasSocios
                        .Average(rs => (decimal?)rs.Calificacion)
                });
        }


        return View(await rutinas.ToListAsync());
    }

    // Acción GET para mostrar los detalles de una rutina específica
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rutina = await _context.Rutinas
            .Include(r => r.TipoRutina)
            .Include(r => r.RutinasEjercicios)
                .ThenInclude(re => re.Ejercicio)
            .Include(r => r.RutinasSocios)
                .ThenInclude(rs => rs.Socio)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (rutina == null)
        {
            return NotFound();
        }

        rutina.PromedioCalif = rutina.RutinasSocios.Average(rs => (decimal?)rs.Calificacion);

        return View(rutina);
    }

    // Acción GET para mostrar el formulario de creación de una nueva rutina
    public IActionResult Create()
    {
        ViewBag.TipoRutinaId = new SelectList(_context.TipoRutinas, "Id", "Nombre");
        ViewBag.Ejercicios = _context.Ejercicios.Select(e => new SelectListItem
        {
            Value = e.Id.ToString(),
            Text = e.Nombre
        }).ToList();
        return View();
    }

    // Acción POST para crear una nueva rutina
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,TipoRutinaId")] Rutina rutina, int[] selectedEjercicios)
    {
        if (ModelState.IsValid)
        {
            _context.Add(rutina);
            await _context.SaveChangesAsync();

            if (selectedEjercicios != null)
            {
                foreach (var ejercicioId in selectedEjercicios)
                {
                    var rutinaEjercicio = new RutinaEjercicio
                    {
                        IdRutina = rutina.Id,
                        IdEjercicio = ejercicioId
                    };
                    _context.RutinasEjercicios.Add(rutinaEjercicio);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        ViewBag.TipoRutinaId = new SelectList(_context.TipoRutinas, "Id", "Nombre", rutina.TipoRutinaId);
        ViewBag.Ejercicios = _context.Ejercicios.Select(e => new SelectListItem
        {
            Value = e.Id.ToString(),
            Text = e.Nombre
        }).ToList();
        return View(rutina);
    }

    // Acción GET para mostrar el formulario de edición de una rutina existente
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rutina = await _context.Rutinas
            .Include(r => r.RutinasEjercicios)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (rutina == null)
        {
            return NotFound();
        }

        ViewBag.TipoRutinaId = new SelectList(_context.TipoRutinas, "Id", "Nombre", rutina.TipoRutinaId);

        var ejercicios = _context.Ejercicios.ToList();
        var selectedEjercicios = rutina.RutinasEjercicios.Select(re => re.IdEjercicio).ToList();
        ViewBag.Ejercicios = ejercicios.Select(e => new SelectListItem
        {
            Value = e.Id.ToString(),
            Text = e.Nombre,
            Selected = selectedEjercicios.Contains(e.Id)
        }).ToList();

        return View(rutina);
    }

    // Acción POST para editar una rutina existente
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,TipoRutinaId")] Rutina rutina, int[] selectedEjercicios)
    {
        if (id != rutina.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rutina);
                await _context.SaveChangesAsync();

                // Actualizar ejercicios
                var existingEjercicios = _context.RutinasEjercicios.Where(re => re.IdRutina == id).ToList();
                _context.RutinasEjercicios.RemoveRange(existingEjercicios);

                if (selectedEjercicios != null)
                {
                    foreach (var ejercicioId in selectedEjercicios)
                    {
                        var rutinaEjercicio = new RutinaEjercicio
                        {
                            IdRutina = rutina.Id,
                            IdEjercicio = ejercicioId
                        };
                        _context.RutinasEjercicios.Add(rutinaEjercicio);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RutinaExists(rutina.Id))
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
        ViewBag.TipoRutinaId = new SelectList(_context.TipoRutinas, "Id", "Nombre", rutina.TipoRutinaId);
        ViewBag.Ejercicios = _context.Ejercicios.Select(e => new SelectListItem
        {
            Value = e.Id.ToString(),
            Text = e.Nombre
        }).ToList();
        return View(rutina);
    }

    // Acción GET para mostrar el formulario de eliminación de una rutina
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rutina = await _context.Rutinas
            .Include(r => r.TipoRutina)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (rutina == null)
        {
            return NotFound();
        }

        return View(rutina);
    }

    // Acción POST para confirmar la eliminación de una rutina
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var rutina = await _context.Rutinas.FindAsync(id);
        if (rutina != null)
        {
            _context.Rutinas.Remove(rutina);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Método privado para verificar si una rutina existe
    private bool RutinaExists(int id)
    {
        return _context.Rutinas.Any(e => e.Id == id);
    }
}
