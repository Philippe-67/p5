using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using p5.Data;
using p5.Models;

namespace p5.Controllers
{
    public class ReparationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReparationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //ici
        //public IActionResult Index(int voitureId)
        //{
        //    var reparations = _context.Reparation.Where(c => c.VoitureId == voitureId).ToList();
        //    return View(reparations);
        //}

        // GET: Reparations
        public async Task<IActionResult> Index()//Index(int voitureId)
        {
            return _context.Reparation != null ?
                        View(await _context.Reparation.Include(r => r.Voiture).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Reparation'  is null.");
        }

            // GET: Reparations/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reparation == null)
            {
                return NotFound();
            }

            var reparation = await _context.Reparation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reparation == null)
            {
                return NotFound();
            }

            return View(reparation);
        }

        // GET: Reparations/Create
        public async Task<IActionResult> Create()
        {
            var voitureList = await _context.Voiture.ToListAsync();
            //ici creation de la liste "categorie"
            var categories = new List<string>
    {

          "---",
          "Climatisation",
        "Moteur",
        "Carrosserie",
        "Système de freinage",
        "Système d'allumage", 
        "Pneumatique",
    };
            ViewBag.Voitures = new SelectList(voitureList, "Id", "Marque");
            ViewBag.Categories = new SelectList(categories);

            

            return View();
            }

            // POST: Reparations/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(CreateReparationModel createReparationModel)
            {

                _context.Reparation.Add(new Reparation
                {
                    Categorie = createReparationModel.Reparation.Categorie,
                    VoitureId = createReparationModel.Voiture.Id
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // GET: Reparations/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Reparation == null)
                {
                    return NotFound();
                }

                var reparation = await _context.Reparation.FindAsync(id);
                if (reparation == null)
                {
                    return NotFound();
                }
                return View(reparation);
            }

            // POST: Reparations/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Categorie")] Reparation reparation)
            {
                if (id != reparation.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(reparation);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReparationExists(reparation.Id))
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
                return View(reparation);
            }

            // GET: Reparations/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Reparation == null)
                {
                    return NotFound();
                }

                var reparation = await _context.Reparation
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (reparation == null)
                {
                    return NotFound();
                }

                return View(reparation);
            }

            // POST: Reparations/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Reparation == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Reparation'  is null.");
                }
                var reparation = await _context.Reparation.FindAsync(id);
                if (reparation != null)
                {
                    _context.Reparation.Remove(reparation);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ReparationExists(int id)
            {
                return (_context.Reparation?.Any(e => e.Id == id)).GetValueOrDefault();
            }
        }
    } 

