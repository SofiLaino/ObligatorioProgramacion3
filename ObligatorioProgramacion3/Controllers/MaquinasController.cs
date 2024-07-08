using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObligatorioProgramacion3.Datos;
using ObligatorioProgramacion3.Models;
using System.Linq;
using System.Threading.Tasks;

public class MaquinasController : Controller
{
    private readonly ApplicationDbContext _context;

    // Constructor que inyecta el contexto de la base de datos
    public MaquinasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Acción GET para listar todas las máquinas con filtros y ordenación
    public async Task<IActionResult> Index(bool? disponible, string sortOrder, int? tipoMaquinaId, int? localAsociadoId)
    {
        IQueryable<Maquina> maquinasQuery = _context.Maquinas
                                                     .Include(m => m.LocalAsociado)
                                                     .Include(m => m.TipoMaquina);

        // Filtro por disponibilidad
        if (disponible.HasValue)
        {
            maquinasQuery = maquinasQuery.Where(m => m.Disponible == disponible);
        }
        // Filtro por tipo de máquina
        if (tipoMaquinaId.HasValue && tipoMaquinaId.Value != 0)
        {
            maquinasQuery = maquinasQuery.Where(s => s.TipoMaquinaId == tipoMaquinaId);
        }
        // Filtro por local asociado
        if (localAsociadoId.HasValue && localAsociadoId.Value != 0)
        {
            maquinasQuery = maquinasQuery.Where(s => s.LocalAsociadoId == localAsociadoId);
        }

        // Lógica de ordenación
        ViewBag.DateSortParam = sortOrder == "date_asc" ? "date_desc" : "date_asc";

        switch (sortOrder)
        {
            case "date_desc":
                maquinasQuery = maquinasQuery.OrderByDescending(m => m.FechaCompra);
                break;
            case "date_asc":
                maquinasQuery = maquinasQuery.OrderBy(m => m.FechaCompra);
                break;
            default:
                maquinasQuery = maquinasQuery.OrderBy(m => m.Id); // Orden por defecto
                break;
        }

        // Cargar las listas desplegables para los filtros
        var tipoMaquinas = await _context.TipoMaquinas.ToListAsync();
        tipoMaquinas.Insert(0, new TipoMaquina { Id = 0, Nombre = "Todos" });
        ViewBag.TipoMaquinaId = new SelectList(tipoMaquinas, "Id", "Nombre", tipoMaquinaId);

        var locales = await _context.Locales.ToListAsync();
        locales.Insert(0, new Local { Id = 0, Nombre = "Todos" });
        ViewBag.LocalAsociadoId = new SelectList(locales, "Id", "Nombre", localAsociadoId);

        var maquinas = await maquinasQuery.ToListAsync();

        // Cargar la lista desplegable de disponibilidad
        ViewBag.Disponible = new SelectList(new[]
        {
        new { Value = "", Text = "Todos" },
        new { Value = "true", Text = "Disponible" },
        new { Value = "false", Text = "No Disponible" }
        }, "Value", "Text", disponible);

        return View(maquinas);
    }

    // Método para manejar el redireccionamiento del filtro de disponibilidad
    public IActionResult FilterByDisponibilidad(bool? disponible, int? tipoMaquinaId, int? localAsociadoId)
    {
        return RedirectToAction(nameof(Index), new { disponible, tipoMaquinaId, localAsociadoId });
    }

    // Acción GET para mostrar los detalles de una máquina
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var maquina = await _context.Maquinas
            .Include(m => m.LocalAsociado)
            .Include(m => m.TipoMaquina)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (maquina == null)
        {
            return NotFound();
        }

        // Calcular la vida útil restante
        var fechaActual = DateTime.Now;
        var fechaCompra = maquina.FechaCompra;

        var vidaUtilTotal = maquina.VidaUtil;
        var vidaUtilTranscurrida = (fechaActual.Year - fechaCompra.Year) * 12 + fechaActual.Month - fechaCompra.Month;

        var vidaUtilRestante = vidaUtilTotal * 12 - vidaUtilTranscurrida;
        var vidaUtilRestanteAnios = vidaUtilRestante / 12;
        var vidaUtilRestanteMeses = vidaUtilRestante % 12;

        ViewBag.VidaUtilRestanteAnios = vidaUtilRestanteAnios;
        ViewBag.VidaUtilRestanteMeses = vidaUtilRestanteMeses;

        return View(maquina);
    }

    // Acción GET para mostrar el formulario de creación de una nueva máquina
    public IActionResult Create()
    {
        ViewBag.Locales = new SelectList(_context.Locales.ToList(), "Id", "Nombre");
        ViewBag.TiposMaquina = new SelectList(_context.TipoMaquinas.ToList(), "Id", "Nombre");
        return View();
    }

    // Acción POST para crear una nueva máquina
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,FechaCompra,PrecioCompra,VidaUtil,Disponible,LocalAsociadoId,TipoMaquinaId")] Maquina maquina)
    {
        if (ModelState.IsValid)
        {
            _context.Add(maquina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Locales = new SelectList(_context.Locales, "Id", "Nombre");
        ViewBag.TiposMaquina = new SelectList(_context.TipoMaquinas, "Id", "Nombre");
        return View(maquina);
    }

    // Acción GET para mostrar el formulario de edición de una máquina existente
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var maquina = await _context.Maquinas
            .Include(m => m.LocalAsociado)
            .Include(m => m.TipoMaquina)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (maquina == null)
        {
            return NotFound();
        }
        ViewBag.Locales = new SelectList(_context.Locales, "Id", "Nombre", maquina.LocalAsociadoId);
        ViewBag.TipoMaquinas = new SelectList(_context.TipoMaquinas, "Id", "Nombre", maquina.TipoMaquinaId);
        return View(maquina);
    }

    // Acción POST para editar una máquina existente
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,FechaCompra,PrecioCompra,VidaUtil,Disponible,LocalAsociadoId,TipoMaquinaId")] Maquina maquina)
    {
        if (id != maquina.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(maquina);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaquinaExists(maquina.Id))
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
        ViewBag.Locales = new SelectList(_context.Locales, "Id", "Nombre", maquina.LocalAsociadoId);
        ViewBag.TipoMaquinas = new SelectList(_context.TipoMaquinas, "Id", "Nombre", maquina.TipoMaquinaId);
        return View(maquina);
    }

    // Acción GET para mostrar el formulario de eliminación de una máquina
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var maquina = await _context.Maquinas
            .Include(m => m.LocalAsociado)
            .Include(m => m.TipoMaquina)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (maquina == null)
        {
            return NotFound();
        }

        return View(maquina);
    }

    // Acción POST para confirmar la eliminación de una máquina
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var maquina = await _context.Maquinas.FindAsync(id);
        if (maquina != null)
        {
            _context.Maquinas.Remove(maquina);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Método privado para verificar si una máquina existe
    private bool MaquinaExists(int id)
    {
        return _context.Maquinas.Any(e => e.Id == id);
    }
}
