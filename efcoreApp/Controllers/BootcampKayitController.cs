using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{

    public class BootcampKayitController : Controller
    {
        private readonly DataContext _context;

        public BootcampKayitController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var bootcampkayitlari = await _context.KursKayitlari
                                                  .Include(x => x.Ogrenci)
                                                  .Include(x => x.Bootcamp)
                                                  .ToListAsync();
            return View(bootcampkayitlari);
        }

        public async Task<IActionResult> Create()
        {

            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Bootcamps = new SelectList(await _context.Bootcamps.ToListAsync(), "KursId", "Baslik");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BootcampKayit model)
        {
            var ogrenci = await _context.Ogrenciler.FindAsync(model.OgrenciId);
            var bootcamp = await _context.Bootcamps.FindAsync(model.KursId);

            if (ogrenci == null || bootcamp == null)
            {
                ModelState.AddModelError("", "Öğrenci veya Bootcamp bulunamadı.");
                return View(model);
            }

            model.Ogrenci = ogrenci;
            model.Bootcamp = bootcamp;
            model.KayitTarihi = DateTime.Now;
            _context.KursKayitlari.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}