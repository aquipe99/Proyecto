using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.DataAccess.Servicios
{
    public class ConnectionService
    {
        public string ConnectionString { get; }
        public ConnectionService(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("DefaultConnection", "La cadena de conexión no puede ser nula.");
        }  
    }
}
