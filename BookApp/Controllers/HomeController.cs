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

        if(!String.IsNullOrEmpty(searchString)){
            ViewBag.SearchString = searchString;
            products = products.Where(p=>p.Name.ToLower().Contains(searchString)).ToList();
        }

        if(!String.IsNullOrEmpty(category) && category != "0"){
            products = products.Where(p=>p.CategoryId == int.Parse(category)).ToList();
        }

       // ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId","Name",category);

        var model = new ProductViewModel{
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId","Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
        var allowenExtensions = new[] {".jpg",".png",".jpeg"};
        var extension = Path.GetExtension(imageFile.FileName);
        var randomfileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
        var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",randomfileName);

        if(imageFile != null){
            if(!allowenExtensions.Contains(extension)){
                ModelState.AddModelError("","Geçerli bir resim seçiniz.");
            }
        }

        if(ModelState.IsValid){
        
        using(var stream = new FileStream(path,FileMode.Create)){
            await imageFile.CopyToAsync(stream);
        }
        model.Image = randomfileName;
        model.ProductId = Repository.Products.Count + 1;
        Repository.CreateProduct(model);
        return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId","Name");
        return View(model);    
    }

}
