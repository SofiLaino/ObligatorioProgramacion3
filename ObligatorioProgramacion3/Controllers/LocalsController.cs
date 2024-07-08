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
    public class LocalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor que inyecta el contexto de la base de datos
        public LocalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción GET para listar todos los locales
        public async Task<IActionResult> Index()
        {
            var locales = await _context.Locales
                .Include(l => l.Ciudad)
                .Include(l => l.Responsable)
                .ToListAsync();
            return View(locales);
        }

        // Acción GET para mostrar los detalles de un local
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locales
                .Include(l => l.Ciudad)
                .Include(l => l.Responsable)
                .Include(l => l.Maquinas)
                    .ThenInclude(m => m.TipoMaquina) // Incluir el TipoMaquina de cada máquina
                .FirstOrDefaultAsync(m => m.Id == id);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // Acción GET para mostrar el formulario de creación de un nuevo local
        public IActionResult Create()
        {
            ViewBag.Ciudades = new SelectList(_context.Ciudades, "Id", "Nombre");
            ViewBag.Responsables = new SelectList(_context.Responsables, "Id", "Nombre");
            return View();
        }

        // Acción POST para crear un nuevo local
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Direccion,Telefono,CiudadId,ResponsableId")] Local local)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el responsable ya está asignado a otro local
                var responsableAsignado = await _context.Locales
                    .AnyAsync(l => l.ResponsableId == local.ResponsableId);
                if (responsableAsignado)
                {
                    ModelState.AddModelError("ResponsableId", "El responsable ya está asignado a otro local.");
                    ViewBag.Ciudades = new SelectList(_context.Ciudades, "Id", "Nombre", local.CiudadId);
                    ViewBag.Responsables = new SelectList(_context.Responsables, "Id", "Nombre", local.ResponsableId);
                    return View(local);
                }

                _context.Add(local);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Si ModelState no es válido, cargar nuevamente las listas desplegables
            ViewBag.Ciudades = new SelectList(_context.Ciudades, "Id", "Nombre", local.CiudadId);
            ViewBag.Responsables = new SelectList(_context.Responsables, "Id", "Nombre", local.ResponsableId);

            return View(local);
        }

        // Acción GET para mostrar el formulario de edición de un local existente
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locales
                .Include(l => l.Ciudad)
                .Include(l => l.Responsable)
                .FirstOrDefaultAsync(l => l.Id == id);
            if (local == null)
            {
                return NotFound();
            }

            ViewBag.Ciudades = new SelectList(_context.Ciudades, "Id", "Nombre", local.CiudadId);
            ViewBag.Responsables = new SelectList(_context.Responsables, "Id", "Nombre", local.ResponsableId);

            return View(local);
        }

        // Acción POST para editar un local existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,Telefono,CiudadId,ResponsableId")] Local local)
        {
            if (id != local.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Verificar si el responsable ya está asignado a otro local
                var responsableAsignado = await _context.Locales
                    .AnyAsync(l => l.ResponsableId == local.ResponsableId && l.Id != local.Id);
                if (responsableAsignado)
                {
                    ModelState.AddModelError("ResponsableId", "El responsable ya está asignado a otro local.");
                    ViewBag.Ciudades = new SelectList(_context.Ciudades, "Id", "Nombre", local.CiudadId);
                    ViewBag.Responsables = new SelectList(_context.Responsables, "Id", "Nombre", local.ResponsableId);
                    return View(local);
                }
                try
                {
                    _context.Update(local);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalExists(local.Id))
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

            ViewBag.Ciudades = new SelectList(_context.Ciudades, "Id", "Nombre", local.CiudadId);
            ViewBag.Responsables = new SelectList(_context.Responsables, "Id", "Nombre", local.ResponsableId);

            return View(local);
        }

        // Acción GET para mostrar el formulario de eliminación de un local
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var local = await _context.Locales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (local == null)
            {
                return NotFound();
            }

            return View(local);
        }

        // Acción POST para confirmar la eliminación de un local
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var local = await _context.Locales.FindAsync(id);
            if (local != null)
            {
                _context.Locales.Remove(local);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método privado para verificar si un local existe
        private bool LocalExists(int id)
        {
            return _context.Locales.Any(e => e.Id == id);
        }
    }
}
