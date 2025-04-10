using Microsoft.Data.SqlClient;
using mvc.DataAccess.Servicios;
using mvc.Entities.ProductoEntites;
using mvc.Entities.UsuarioEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.DataAccess.DAUsuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;
        public UsuarioRepository(ConnectionService connectionService)
        {

            _connectionString = connectionService.ConnectionString;
        }
        public Usuario ObtenerPorUsuarioGmail(string gmail)
        {            
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT [id_usuario], [correo], [contrasenia] FROM [dbo].[usuarios] WHERE [correo] = @gmail";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gmail", gmail);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var usuario = new Usuario
                                {
                                    Id = reader.GetInt32(0),
                                    Correo = reader.GetString(1),
                                    Contrasenia= reader.GetString(2)
                                };

                                return usuario;
                            }
                        }
                    }
                    return new Usuario();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error: ", ex);
                }
            }
        }
    }
}
