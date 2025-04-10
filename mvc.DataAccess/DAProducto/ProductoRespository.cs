using mvc.Entities.ProductoEntites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using mvc.DataAccess.Servicios;

namespace mvc.DataAccess.DAProducto
{
    public class ProductoRespository : IProductoRepository    {

        private readonly string _connectionString;

        public ProductoRespository(ConnectionService connectionService) {

            _connectionString = connectionService.ConnectionString;
        }

        public ObservableCollection<Producto> ObtenerTodos()
        {
            var productos = new ObservableCollection<Producto>();  
            using (var connection = new SqlConnection(_connectionString)) {
                try
                { 
                   connection.Open();
                    string query = "SELECT [id], [nombre], [precio] FROM [dbo].[producto]";
                    using (var command = new SqlCommand(query,connection)) {
                        using (var reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                var producto = new Producto
                                {
                                    Id = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Precio = reader.GetDecimal(2),
                                };
                                productos.Add(producto);
                            }
                        }
                    }
                    return productos;
                    
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: ", ex);
                }
            }
        }     
    }
}
