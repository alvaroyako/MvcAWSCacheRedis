using MvcAWSCacheRedis.Helper;
using MvcAWSCacheRedis.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAWSCacheRedis.Services
{
    public class ServiceCacheAWS
    {
        private IDatabase cache;

        public ServiceCacheAWS()
        {
            this.cache = CacheRedisMultiplexer.Connection.GetDatabase();
        }

        public void AddProducto(Producto producto)
        {
            List<Producto> productos;
            string json = this.cache.StringGet("productoscache");
            if (json == null)
            {
                productos = new List<Producto>();
            }
            else
            {
                productos = JsonConvert.DeserializeObject<List<Producto>>(json);
            }
            productos.Add(producto);
            json = JsonConvert.SerializeObject(productos);
            this.cache.StringSet("productoscache", json, TimeSpan.FromMinutes(30));
        }

        public List<Producto> GetProductosCache()
        {
            string json = this.cache.StringGet("productoscache");
            if (json == null)
            {
                return null;
            }
            else
            {
                List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(json);
                return productos;
            }
        }

        public void EliminarProductoCache(int idProducto)
        {
            List<Producto> productos = this.GetProductosCache();
            if (productos != null)
            {
                Producto producto = productos.SingleOrDefault(x => x.IdProducto == idProducto);
                productos.Remove(producto);
                if (productos.Count == 0)
                {
                    this.cache.KeyDelete("productoscache");
                }
                else
                {
                    String json = JsonConvert.SerializeObject(productos);
                    this.cache.StringSet("productoscache", json, TimeSpan.FromMinutes(30));
                }
            }
        }
    }
}
