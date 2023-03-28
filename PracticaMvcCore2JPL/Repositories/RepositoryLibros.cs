using PracticaMvcCore2JPL.Data;
using PracticaMvcCore2JPL.Models;

namespace PracticaMvcCore2JPL.Repositories
{
    public class RepositoryLibros
    {
        private BibliotecaContext context;

        public RepositoryLibros(BibliotecaContext context)
        {
            this.context = context;
        }

        //Funcion para sacar todos los libros
        public List<Libro> GetAllLibros()
        {
            var consulta = from datos in context.Libros
                           select datos;
            return consulta.ToList();
        }

        //Funcion para sacar los libros por su id
        public Libro GetLibrosId(int idLibro)
        {
            var consulta = from datos in context.Libros
                           where datos.IdLibro == idLibro
                           select datos;
            return consulta.FirstOrDefault();

        }

        //Funcion para sacar cada libro por su genero
        public List<Libro> LibrosGenero(int idgenero)
        {
            var consulta = from datos in context.Libros
                           where datos.IdGenero == idgenero
                           select datos;
            return consulta.ToList();
        }

        //Funcion para libros carrito
        public List<Libro> GetLibrosCarrito(List<int> idLibros)
        {

            var consulta = from datos in context.Libros
                           where idLibros.Contains(datos.IdLibro)
                           select datos;
            return consulta.ToList();
        }


    }
}
