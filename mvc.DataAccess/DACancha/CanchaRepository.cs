using Azure;
using Microsoft.Data.SqlClient;
using mvc.DataAccess.Servicios;
using mvc.Entities.BaseEntities.UsuarioEntities;
using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.MenuEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DataAccess.DACancha
{
    public class CanchaRepository : ICanchaRepository
    {
        private readonly string _connectionString;
        public CanchaRepository(ConnectionService connectionService) { 
            _connectionString = connectionService.ConnectionString;
        }

        public bool GuardarCancha(Cancha cancha)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_CANCHA_INSERT_UPDATE", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@ID", SqlDbType.Int, 4).Value = cancha.Id;
                command.Parameters.Add("@NOMBRE", SqlDbType.NVarChar, 20).Value = cancha.Nombre;
                command.Parameters.Add("@DESCRIPCION", SqlDbType.NVarChar, 100).Value = cancha.Descipcion;
                command.Parameters.Add("@USUARIO", SqlDbType.Int, 4).Value = cancha.UsuarioModifica;
                command.Parameters.Add("@ESTADO", SqlDbType.Int, 4).Value = cancha.Estado;
                connection.Open();               
                int result = command.ExecuteNonQuery();

                return result > 0;
            }
            catch (Exception ex)
            {

                throw new Exception("Error: ", ex);
            }
        }

        public Cancha ObtenerCanchaPorId(int Id)
        {
            try
            {

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_CANCHA_SELECT_BUSCARPOR_ID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@ID", SqlDbType.Int,4).Value = Id;

                connection.Open();
                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Cancha
                    {
                        Id = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                        Nombre = reader["NOMBRE"] != DBNull.Value ? reader["NOMBRE"].ToString() : string.Empty,
                        Descipcion = reader["DESCRIPCION"] != DBNull.Value ? reader["DESCRIPCION"].ToString() : string.Empty,
                        Estado = reader["ESTADO"] != DBNull.Value ? Convert.ToBoolean(reader["ESTADO"]) : false 
                    };
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("Error: ", ex);
            }
        }

        public ObservableCollection<Cancha> ObtenerListaCanchas(int page, int pageSize, string buscar)
        {

            try
            {
                var canchas = new ObservableCollection<Cancha>();
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_CANCHA_SELECT_LISTA_CANCHAS", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@PAGE", SqlDbType.Int, 4).Value = page;
                command.Parameters.Add("@PAGESIZE", SqlDbType.Int, 4).Value = pageSize;
                if (string.IsNullOrWhiteSpace(buscar))
                {
                    command.Parameters.Add("@BUSCAR", SqlDbType.NVarChar,20).Value = buscar;
                }
                else {
                    command.Parameters.Add("@BUSCAR", SqlDbType.NVarChar, 20).Value = buscar.Trim();
                }

                    connection.Open();
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var cancha = new Cancha
                    {
                        Id = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                        Nombre = reader["NOMBRE"] != DBNull.Value ? reader["NOMBRE"].ToString() : string.Empty,
                        Descipcion = reader["DESCRIPCION"] != DBNull.Value ? reader["DESCRIPCION"].ToString() : string.Empty,
                        Total = reader["TOTAL"] != DBNull.Value ? Convert.ToInt32(reader["TOTAL"]) : 0,
                    };
                    canchas.Add(cancha);
                }
                return canchas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }

        }

        public bool ValidarCanchaNombre(string nombre, int Id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_CANCHA_SELECT_BUSCARPOR_NOMBRE", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };               
                command.Parameters.Add("@NOMBRE", SqlDbType.NVarChar, 20).Value = nombre;
                command.Parameters.Add("@ID", SqlDbType.Int,4).Value = Id;

                connection.Open();
                object result = command.ExecuteScalar();

                return result != null && Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {

                throw new Exception("Error: ", ex);
            }
        }
        public bool EliminarCanchaPorId(int Id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_CANCHA_DELETE_POR_ID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@ID", SqlDbType.Int, 4).Value = Id;
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
