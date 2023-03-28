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
        //FUNCION PARA LA PAGINACION
        public Libro GetPaginados(int posicion, ref int numeroLibros)
        {
            List<Libro> libros = this.GetAllLibros();
            numeroLibros = libros.Count;
            Libro libro =
                libros.Skip(posicion).Take(1).FirstOrDefault();
            return libro;
        }

        private int GetMaximoIdPedido()
        {
            var maximo = (from datos in this.context.Pedidos
                          select datos).Max(x => x.IdPedido) + 1;
            return maximo;
        }


        private int GetMaximoIdFactura()
        {
            var maximo = (from datos in this.context.Pedidos
                          select datos).Max(x => x.IdFactura) + 1;
            return maximo;
        }

        /*public async Task insertLibrosAsync( int idlibro)
        {
            Pedido ped = new Pedido();
            int idPedido = this.GetMaximoIdPedido();
            int idFactura = this.GetMaximoIdFactura();

            ped.IdPedido = idPedido;

        }*/
    }
}
