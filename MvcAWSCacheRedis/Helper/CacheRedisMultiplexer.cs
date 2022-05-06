using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAWSCacheRedis.Helper
{
    public class CacheRedisMultiplexer
    {
        private static Lazy<ConnectionMultiplexer> GenerarConexion = new Lazy<ConnectionMultiplexer>(() =>
          {
              return ConnectionMultiplexer.Connect("cache-productos-amh.5srfja.ng.0001.use1.cache.amazonaws.com:6379");
          });

        public static ConnectionMultiplexer Connection
        {
            get { return GenerarConexion.Value; }
        }
    }
}
