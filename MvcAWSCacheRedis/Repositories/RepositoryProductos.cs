using MvcAWSCacheRedis.Helper;
using MvcAWSCacheRedis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcAWSCacheRedis.Repositories
{
    public class RepositoryProductos
    {
        private XDocument xDocument;

        public RepositoryProductos(PathProvider pathProvider)
        {
            string path = pathProvider.MapPath("productos.xml", Folders.Documents);
            this.xDocument = XDocument.Load(path);
        }

        public List<Producto> GetProductos()
        {
            var consulta = from datos in this.xDocument.Descendants("producto")
                           select new Producto
                           {
                               IdProducto = int.Parse(datos.Element("idproducto").Value),
                               Nombre = datos.Element("nombre").Value,
                               Descripcion = datos.Element("descripcion").Value,
                               Precio = int.Parse(datos.Element("precio").Value),
                               Imagen = datos.Element("imagen").Value
                           };
            return consulta.ToList();
        }

        public Producto FindProducto(int id)
        {
            return this.GetProductos().FirstOrDefault(z => z.IdProducto == id);
        }
    }
}
