using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JPL.Models;
using PracticaMvcCore2JPL.Repositories;

namespace PracticaMvcCore2JPL.ViewComponents
{
    public class DesplegableGenerosViewComponent : ViewComponent
    {
        private RepositoryGeneros repo;

        public DesplegableGenerosViewComponent(RepositoryGeneros repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = this.repo.GetAllGeneros();
            return View(generos);
        }
    }
}