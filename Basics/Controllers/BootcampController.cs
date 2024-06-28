using Basics.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basics.Controllers;

public class BootcampController : Controller{

    public IActionResult Details(int? id){
        if(id==null){
            return Redirect("/Bootcamp/List");
        }
        var bootcamp = Repository.GetById(id);

        return View(bootcamp);

    }

    public IActionResult List(){
        return View(Repository.Bootcamps);
    }

}