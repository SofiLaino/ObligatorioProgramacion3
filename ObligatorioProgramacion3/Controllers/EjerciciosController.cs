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
    public class EjerciciosController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor del controlador que inyecta el contexto de la base de datos
        public EjerciciosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para listar todos los ejercicios
        // GET: Ejercicios
        public async Task<IActionResult> Index()
        {
            var ejercicios = await _context.Ejercicios.Include(e => e.Maquina).ToListAsync();
            return View(ejercicios);
        }

        // Acción para mostrar los detalles de un ejercicio específico
        // GET: Ejercicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejercicio = await _context.Ejercicios
                .Include(e => e.Maquina)
                .Include(e => e.RutinasEjercicios)
                .ThenInclude(re => re.Rutina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ejercicio == null)
            {
                return NotFound();
            }

            return View(ejercicio);
        }

        // Acción para mostrar el formulario de creación de un nuevo ejercicio
        // GET: Ejercicios/Create
        public IActionResult Create()
        {
            ViewData["MaquinaId"] = new SelectList(_context.Maquinas, "Id", "Nombre");
            return View();
        }

        // Acción para manejar el envío del formulario de creación de un nuevo ejercicio
        // POST: Ejercicios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,MaquinaId")] Ejercicio ejercicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ejercicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaquinaId"] = new SelectList(_context.Maquinas, "Id", "Nombre", ejercicio.MaquinaId);
            return View(ejercicio);
        }

        // Acción para mostrar el formulario de edición de un ejercicio existente
        // GET: Ejercicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejercicio = await _context.Ejercicios.FindAsync(id);
            if (ejercicio == null)
            {
                return NotFound();
            }
            ViewData["MaquinaId"] = new SelectList(_context.Maquinas, "Id", "Nombre", ejercicio.MaquinaId);
            return View(ejercicio);
        }

        // Acción para manejar el envío del formulario de edición de un ejercicio existente
        // POST: Ejercicios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,MaquinaId")] Ejercicio ejercicio)
        {
            if (id != ejercicio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ejercicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EjercicioExists(ejercicio.Id))
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
            ViewData["MaquinaId"] = new SelectList(_context.Maquinas, "Id", "Nombre", ejercicio.MaquinaId);
            return View(ejercicio);
        }

        // Acción para mostrar la confirmación de eliminación de un ejercicio
        // GET: Ejercicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejercicio = await _context.Ejercicios
                .Include(e => e.Maquina)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ejercicio == null)
            {
                return NotFound();
            }

            return View(ejercicio);
        }

        // Acción para manejar la confirmación de eliminación de un ejercicio
        // POST: Ejercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ejercicio = await _context.Ejercicios.FindAsync(id);
            if (ejercicio != null)
            {
                _context.Ejercicios.Remove(ejercicio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método privado para verificar si un ejercicio existe
        private bool EjercicioExists(int id)
        {
            return _context.Ejercicios.Any(e => e.Id == id);
        }
    }
}
