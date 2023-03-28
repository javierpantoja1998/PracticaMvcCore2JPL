using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2JPL.Data;
using PracticaMvcCore2JPL.Models;

namespace PracticaMvcCore2JPL.Repositories
{
    public class RepositoryUsuarios
    {
        private BibliotecaContext context;

        public RepositoryUsuarios(BibliotecaContext context)
        {
            this.context = context;
        }

        //FUNCION PARA BUSCAR USUARIO por nombre y password
        public Usuario GetUserByNamePass(string email, string password)
        {
            return this.context.Usuarios.Where
                (x => x.Email == email && x.Password == password).AsEnumerable().FirstOrDefault();
        }

       
    }
}
