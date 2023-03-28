using PracticaMvcCore2JPL.Data;
using PracticaMvcCore2JPL.Models;

namespace PracticaMvcCore2JPL.Repositories
{
    public class RepositoryGeneros
    {
        private BibliotecaContext context;

        public RepositoryGeneros(BibliotecaContext context)
        {
            this.context = context;
        }

        public List<Genero> GetAllGeneros()
        {
            var consulta = from datos in context.Generos
                           select datos;
            return consulta.ToList();
        }
    }
}
