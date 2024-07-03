using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookApp.Controllers;

public class HomeController : Controller
{

    public HomeController()
    {
    }

    [HttpGet]
    public IActionResult Index(string searchString, string category)
    {
        var products = Repository.Products;

        if (!String.IsNullOrEmpty(searchString))
        {
            ViewBag.SearchString = searchString;
            products = products.Where(p => p.Name.ToLower().Contains(searchString)).ToList();
        }

        if (!String.IsNullOrEmpty(category) && category != "0")
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        // ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId","Name",category);

        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
        var allowenExtensions = new[] { ".jpg", ".png", ".jpeg" };
        if (imageFile != null)
        {
            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowenExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Geçerli bir resim seçiniz.");
            }
            else
            {
                var randomfileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomfileName);

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    model.Image = randomfileName;
                }
                catch
                {
                    ModelState.AddModelError("", "Dosya yüklenirken bir hata oluştu.");
                }

                if (!allowenExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Geçerli bir resim seçiniz.");
                }
            }
        }
        else
        {
            ModelState.AddModelError("", "Bir resim dosyası seçiniz.");
        }
        if (ModelState.IsValid)
        {
            model.ProductId = Repository.Products.Count + 1;
            Repository.CreateProduct(model);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(model);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var entitiy = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if (entitiy == null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(entitiy);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile){
        
        if(id!=model.ProductId){
            return NotFound();
        }

        var allowenExtensions = new[] { ".jpg", ".png", ".jpeg" };
        if (imageFile != null)
        {
            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!allowenExtensions.Contains(extension))
            {
                ModelState.AddModelError("", "Geçerli bir resim seçiniz.");
            }
            else
            {
                var randomfileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomfileName);

                try
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    model.Image = randomfileName;
                }
                catch
                {
                    ModelState.AddModelError("", "Dosya yüklenirken bir hata oluştu.");
                }

                if (!allowenExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Geçerli bir resim seçiniz.");
                }
            }
        }
        else
        {
            ModelState.AddModelError("", "Bir resim dosyası seçiniz.");
        }
        if (ModelState.IsValid)
        {
            Repository.EditProduct(model);
            return RedirectToAction("index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
        return View(model);
    }

    public IActionResult Delete(int? id){
        if(id == null){
            return NotFound();
        }
        var entitiy = Repository.Products.FirstOrDefault(p => p.ProductId == id);
        if(entitiy == null){
            return NotFound();
        }

        Repository.DeleteProduct(entitiy);
        return RedirectToAction("index");

    }

}
