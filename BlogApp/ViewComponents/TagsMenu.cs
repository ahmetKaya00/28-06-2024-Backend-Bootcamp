using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents{

    public class TagsMenu : ViewComponent
    {
         private ITagRepository _tatgRepository;

        public TagsMenu(ITagRepository tagRepository){
            _tatgRepository = tagRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            return View(await _tatgRepository.Tags.ToListAsync());
        }
    }
}