using Microsoft.AspNetCore.Mvc;
using MvcAWSCacheRedis.Models;
using MvcAWSCacheRedis.Repositories;
using MvcAWSCacheRedis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAWSCacheRedis.Controllers
{
    public class ProductosController : Controller
    {
        private RepositoryProductos repo;
        private ServiceCacheAWS service;

        public ProductosController(RepositoryProductos repo,ServiceCacheAWS service)
        {
            this.repo = repo;
            this.service = service;
        }

        public IActionResult Index()
        {
            List<Producto> productos = this.repo.GetProductos();
            return View(productos);
        }

        public IActionResult Details(int id)
        {
            Producto producto = this.repo.FindProducto(id);
            return View(producto);
        }

        public IActionResult SeleccionarFavorito(int id)
        {
            Producto producto = this.repo.FindProducto(id);
            this.service.AddProducto(producto);
            TempData["MENSAJE"] = "Producto " + producto.Nombre + " almacenado como Favorito";
            return RedirectToAction("Index");
        }

        public IActionResult Favoritos()
        {
            List<Producto> productos = this.service.GetProductosCache();
            return View(productos); 
        }

        public IActionResult EliminarFavorito(int id)
        {
            this.service.EliminarProductoCache(id);
            return RedirectToAction("Favoritos");
        }
    }
}
