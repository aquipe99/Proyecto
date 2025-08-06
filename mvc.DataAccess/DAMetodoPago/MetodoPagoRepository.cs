using Microsoft.Data.SqlClient;
using SR.DataAccess.Servicios;
using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.MetodoPagoEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DataAccess.DAMetodoPago
{
    public class MetodoPagoRepository : IMetodoPagoRepository
    {
        private readonly string _connectionString;
        public MetodoPagoRepository(ConnectionService connectionService)
        {
            _connectionString = connectionService.ConnectionString;
        }
        public bool GuardarMetodoPago(MetodoPago metodoPago)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_METODO_PAGO_INSERT_UPDATE", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@ID", SqlDbType.Int, 4).Value = metodoPago.Id;
                command.Parameters.Add("@NOMBRE", SqlDbType.NVarChar, 50).Value = metodoPago.Nombre;              
                command.Parameters.Add("@USUARIO", SqlDbType.Int, 4).Value = metodoPago.UsuarioModifica;               
                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {

                throw new Exception("Error: ", ex);
            }
        }
        public MetodoPago ObtenerMetodoPagoPorId(int Id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_METODO_PAGO_SELECT_BUSCARPOR_ID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@ID", SqlDbType.Int, 4).Value = Id;

                connection.Open();
                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new MetodoPago
                    {
                        Id = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                        Nombre = reader["NOMBRE"] != DBNull.Value ? reader["NOMBRE"].ToString() : string.Empty,
                    };
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("Error: ", ex);
            }
        }
        public ObservableCollection<MetodoPago> ObtenerListaMetodoPago(int page, int pageSize, string buscar)
        {
            try
            {
                var metodoPagos = new ObservableCollection<MetodoPago>();
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_METODO_PAGO_SELECT_LISTA_CANCHAS", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@PAGE", SqlDbType.Int, 4).Value = page;
                command.Parameters.Add("@PAGESIZE", SqlDbType.Int, 4).Value = pageSize;
                if (string.IsNullOrWhiteSpace(buscar))
                {
                    command.Parameters.Add("@BUSCAR", SqlDbType.NVarChar, 20).Value = buscar;
                }
                else
                {
                    command.Parameters.Add("@BUSCAR", SqlDbType.NVarChar, 20).Value = buscar.Trim();
                }

                connection.Open();
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var metodoPago = new MetodoPago
                    {
                        Id = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                        Nombre = reader["NOMBRE"] != DBNull.Value ? reader["NOMBRE"].ToString() : string.Empty,                   
                        Total = reader["TOTAL"] != DBNull.Value ? Convert.ToInt32(reader["TOTAL"]) : 0,
                    };
                    metodoPagos.Add(metodoPago);
                }
                return metodoPagos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
        public bool ValidarMetodoPagoNombre(string nombre, int Id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_METODO_PAGO_SELECT_BUSCARPOR_NOMBRE", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@NOMBRE", SqlDbType.NVarChar, 20).Value = nombre;
                command.Parameters.Add("@ID", SqlDbType.Int, 4).Value = Id;

                connection.Open();
                object result = command.ExecuteScalar();

                return result != null && Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {

                throw new Exception("Error: ", ex);
            }
        }
        public bool EliminarMetodoPagoPorId(int Id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_METODO_PAGO_DELETE_POR_ID", connection)
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

                throw;
            }
        }

        public ObservableCollection<MetodoPago> ObtenerTodosLosMetodosPago()
        {
            try
            {
                var metodoPagos = new ObservableCollection<MetodoPago>();
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_METODO_PAGO_SELECT_METODOPAGO", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };                

                connection.Open();
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var metodoPago = new MetodoPago
                    {
                        Id = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                        Nombre = reader["NOMBRE"] != DBNull.Value ? reader["NOMBRE"].ToString() : string.Empty                      
                    };
                    metodoPagos.Add(metodoPago);
                }
                return metodoPagos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

    }
}
