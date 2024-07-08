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
    public class ResponsablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor que inyecta el contexto de la base de datos
        public ResponsablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción GET para listar todos los responsables
        public async Task<IActionResult> Index()
        {
            return View(await _context.Responsables.ToListAsync());
        }

        // Acción GET para mostrar los detalles de un responsable específico
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsable = await _context.Responsables
                .Include(r => r.Local)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (responsable == null)
            {
                return NotFound();
            }

            return View(responsable);
        }

        // Acción GET para mostrar el formulario de creación de un nuevo responsable
        public IActionResult Create()
        {
            return View();
        }

        // Acción POST para crear un nuevo responsable
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Telefono")] Responsable responsable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(responsable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(responsable);
        }

        // Acción GET para mostrar el formulario de edición de un responsable existente
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsable = await _context.Responsables.FindAsync(id);
            if (responsable == null)
            {
                return NotFound();
            }
            return View(responsable);
        }

        // Acción POST para editar un responsable existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Telefono")] Responsable responsable)
        {
            if (id != responsable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(responsable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResponsableExists(responsable.Id))
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
            return View(responsable);
        }

        // Acción GET para mostrar el formulario de eliminación de un responsable
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsable = await _context.Responsables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (responsable == null)
            {
                return NotFound();
            }

            return View(responsable);
        }

        // Acción POST para confirmar la eliminación de un responsable
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var responsable = await _context.Responsables.FindAsync(id);
            if (responsable != null)
            {
                _context.Responsables.Remove(responsable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método privado para verificar si un responsable existe
        private bool ResponsableExists(int id)
        {
            return _context.Responsables.Any(e => e.Id == id);
        }
    }
}
