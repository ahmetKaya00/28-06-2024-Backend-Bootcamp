using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers{

    public class EgitmenController:Controller{
        private readonly DataContext _context;

        public EgitmenController(DataContext context){
            _context = context;
        }

        public async Task<IActionResult>Index(){
            return View(await _context.Egitmenler.ToListAsync());
        }

         public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Egitmen model){
            _context.Egitmenler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id){
            if(id == null){
                return NotFound();
            }

            var entity = await _context.Egitmenler.FirstOrDefaultAsync(o=>o.OgretmenId==id);

            if(entity == null){
                return NotFound();
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int? id, Egitmen model){
            if(id != model.OgretmenId){
                return NotFound();
            }

            if(ModelState.IsValid){
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Egitmenler.Any(o=>o.OgretmenId == model.OgretmenId)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id){
            if(id==null){
                return NotFound();
            }

            var egitmen = await _context.Egitmenler.FindAsync(id);
            if(egitmen == null){
                return NotFound();
            }
            return View(egitmen);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id){
            var egitmen = await _context.Egitmenler.FindAsync(id);
            if(egitmen == null){
                return NotFound();
            }
            _context.Egitmenler.Remove(egitmen);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
    }
}