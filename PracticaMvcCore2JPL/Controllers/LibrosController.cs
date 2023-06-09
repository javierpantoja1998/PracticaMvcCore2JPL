﻿using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2JPL.Extensions;
using PracticaMvcCore2JPL.Filters;
using PracticaMvcCore2JPL.Models;
using PracticaMvcCore2JPL.Repositories;

namespace PracticaMvcCore2JPL.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;

        public LibrosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Libro> libros = this.repo.GetAllLibros();
            return View(libros);
        }


        //FUNCION PARA SACAR EL GENERO
        public IActionResult LibrosGenero(int idgenero)
        {
            List<Libro> librosGenero = this.repo.LibrosGenero(idgenero);
            return View(librosGenero);
        }


        public IActionResult LibrosPaginacion(int? posicion)
        {
            if(posicion == null)
            {
                posicion = 0;
            }
            int numeroLibros = 0;
            Libro libro = this.repo.GetPaginados(posicion.Value, ref numeroLibros);
            ViewData["DATOS"] = "Libro " + (posicion+1);
            int siguiente = posicion.Value + 1;
            if(siguiente >= numeroLibros)
            {
                siguiente = 0;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 0)
            {
                anterior = numeroLibros - 1;
            }
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            return View(libro);
        }


        //FUNCION PARA GUARDAR LOS LIBROS EN EL CARRITO
        
        public IActionResult LibroDetalles(int idlibro, int? idlibroCarrito)
        {
            if (idlibroCarrito != null)
            //GUARDAMOS EL PRODUCTO EN EL CARRITO
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("CARRITO") == null)
                {
                    carrito = new List<int>();
                }
                else
                {
                    carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
                }
                carrito.Add(idlibroCarrito.Value);
                HttpContext.Session.SetObject("CARRITO", carrito);


            }
            Libro libro = this.repo.GetLibrosId(idlibro);
            return View(libro);
        }

        //FUNCION PARA PINTAR EL CARRITO Y BORRAR OBJETOS DE EL
        public IActionResult Carrito(int? idlibro) 
        {
            //LE PASAMOS EL CARRITO
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            //TIENES QUE CREAR PARA AÑADIR DATOS AL CARRITO
            if (carrito == null)
            {
                return View();
            }
            else
            {
                if (idlibro != null)
                {
                    carrito.Remove(idlibro.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);
                }
                List<Libro> libros = this.repo.GetLibrosCarrito(carrito);
                return View(libros);
            }
        }

        [AuthorizeUsuarios]
        public IActionResult Pedidos()
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            List<Libro> zapatillas = this.repo.GetLibrosCarrito(carrito);
            HttpContext.Session.Remove("CARRITO");
            return View(zapatillas);
        }
    }
}
