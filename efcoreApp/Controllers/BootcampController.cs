using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers{

    public class BootcampController : Controller{
        private readonly DataContext _context;

        public BootcampController(DataContext context){
            _context = context;
        }

        public async Task<IActionResult> Index(){
            return View(await _context.Bootcamps.Include(b=>b.Egitmen).ToListAsync());
        }

        public async Task<IActionResult> Create(){

            ViewBag.Egitmenler = new SelectList(await _context.Egitmenler.ToListAsync(),"OgretmenId","AdSoyad");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bootcamp model){
            _context.Bootcamps.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
         [HttpGet]
        public async Task<IActionResult> Edit(int? id){
            if(id == null){
                return NotFound();
            }

            var btc = await _context.Bootcamps.Include(b=>b.KursKayitlari).ThenInclude(b=>b.Ogrenci).FirstOrDefaultAsync(o=>o.KursId == id);

            if(btc == null){
                return NotFound();
            }
            ViewBag.Egitmenler = new SelectList(await _context.Egitmenler.ToListAsync(),"OgretmenId","AdSoyad");

            return View(btc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Edit(int? id, BootcampViewModel model){
            if(id != model.KursId){
                return NotFound();
            }

            if(ModelState.IsValid){
                try
                {
                    _context.Update(new Bootcamp(){KursId = model.KursId, Baslik=model.Baslik,EgitmenId=model.EgitmenId});
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!_context.Bootcamps.Any(o=>o.KursId == model.KursId)){
                        return NotFound();
                    }
                    else{
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Egitmenler = new SelectList(await _context.Egitmenler.ToListAsync(),"OgretmenId","AdSoyad");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id){
            if(id==null){
                return NotFound();
            }

            var bootcamp = await _context.Bootcamps.FindAsync(id);
            if(bootcamp == null){
                return NotFound();
            }
            return View(bootcamp);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id){
            var bootcamp = await _context.Bootcamps.FindAsync(id);
            if(bootcamp == null){
                return NotFound();
            }
            _context.Bootcamps.Remove(bootcamp);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
    }
}