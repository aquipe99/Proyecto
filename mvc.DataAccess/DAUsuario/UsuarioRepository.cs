using Microsoft.Data.SqlClient;
using mvc.DataAccess.Servicios;
using mvc.Entities.BaseEntities.UsuarioEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        public Usuario BuscarUsuarioPorCorreo(string correo)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_USUARIO_SELECT_POR_CORREO", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@CORREO",SqlDbType.NVarChar,100).Value= correo;               

                connection.Open();
                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Usuario
                    {
                        Id= reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]):0,
                        Correo = reader["CORREO"] != DBNull.Value ? reader["CORREO"].ToString() : string.Empty,
                        Contrasenia = reader["CONTRASENIA"] != DBNull.Value ? reader["CONTRASENIA"].ToString() : string.Empty,
                        Estado = reader["ESTADO"] != DBNull.Value ? Convert.ToBoolean(reader["ESTADO"]) : false,
                        RolName = reader["ROL_NAME"] != DBNull.Value ? reader["ROL_NAME"].ToString() : string.Empty,
                        Nombre = reader["NOMBRE"] != DBNull.Value ? reader["NOMBRE"].ToString() : string.Empty
                    };
                }
                return  null;
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error: ", ex);
            }
        }
        public bool SaveUsaurio(Usuario usuario)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_USUARIO_INSERT_UPDATE", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@ID", SqlDbType.Int,4).Value = usuario.Id;
                command.Parameters.Add("@NOMBRE", SqlDbType.NVarChar, 20).Value = usuario.Nombre;
                command.Parameters.Add("@TELEFONO", SqlDbType.NVarChar,10).Value = usuario.Telefono;
                command.Parameters.Add("@CORREO", SqlDbType.NVarChar, 100).Value = usuario.Correo;
                command.Parameters.Add("@CONTRASENIA", SqlDbType.NVarChar, 255).Value = usuario.Contrasenia;
                command.Parameters.Add("@ROL_ID", SqlDbType.Int,4).Value = usuario.Rol_id?.Id;
                command.Parameters.Add("@USUARIO", SqlDbType.Int, 4).Value = usuario.UsuarioCrea;
                command.Parameters.Add("@ESTADO", SqlDbType.Int, 4).Value = usuario.Estado;
                connection.Open();               
                int result = command.ExecuteNonQuery();

                return result > 0;
            }
            catch (Exception ex)
            {

                throw new Exception("Error: ", ex);
            }
        }

    }

}
