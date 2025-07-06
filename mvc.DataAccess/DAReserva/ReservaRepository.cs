using Microsoft.Data.SqlClient;
using SR.DataAccess.Servicios;
using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.ReservaEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SR.DataAccess.DAReserva
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly string _connectionString;
        public ReservaRepository(ConnectionService connectionService) {
            _connectionString = connectionService.ConnectionString;
        }
        public bool GuardarReserva(Reserva reserva,out string mensaje)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_RESERVA_INSERT_UPDATE", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@Id", SqlDbType.Int, 4).Value = reserva.Id;
                command.Parameters.Add("@NombreCliente", SqlDbType.NVarChar,50).Value = reserva.NombreCliente;
                command.Parameters.Add("@Fecha", SqlDbType.DateTime, 100).Value = reserva.Fecha;
                command.Parameters.Add("@HoraInicio", SqlDbType.Time).Value = reserva.HoraInicio;
                command.Parameters.Add("@HoraFin", SqlDbType.Time).Value = reserva.HoraFin;
                command.Parameters.Add("@MetodoPagoId", SqlDbType.Int,4).Value = reserva.MetodoPagoId;
                command.Parameters.Add("@TipoPago", SqlDbType.NVarChar, 20).Value = reserva.TipoPago;
                command.Parameters.Add("@CanchaId", SqlDbType.Int).Value = reserva.CanchaId;
                command.Parameters.Add("@MontoTotal", SqlDbType.Decimal).Value = reserva.MontoTotal ;
                command.Parameters.Add("@MontoPagado", SqlDbType.Decimal).Value = reserva.MontoPagado;
                command.Parameters.Add("@Telefono", SqlDbType.NVarChar, 10).Value = reserva.Telefono;
                command.Parameters.Add("@Usuario", SqlDbType.Int, 4).Value = reserva.UsuarioModifica;
                var resultadoParam = new SqlParameter("@Resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
                command.Parameters.Add(resultadoParam);

                connection.Open();
                command.ExecuteNonQuery();
                int resultado = resultadoParam.Value != DBNull.Value ? Convert.ToInt32(resultadoParam.Value) : -99;
                switch (resultado)
                {
                    case 1:
                        mensaje = "Reserva creada con éxito."; return true;
                    case 2:
                        mensaje = "Reserva actualizada con éxito."; return true;
                    case -1:
                        mensaje = "Conflicto: ya existe una reserva en ese horario."; return false;
                    default:
                        mensaje = "Error inesperado."; return false;
                }


            }
            catch (Exception ex)
            {

                throw new Exception("Error: ", ex);
            }
        }

        public ObservableCollection<Reserva> ObtenerReservaPorFecha(DateTime Fecha)
        {
            try
            {
                var reservas = new ObservableCollection<Reserva>();
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_RESERVA_SELECT_PORFECHA", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@FECHA", SqlDbType.DateTime, 100).Value = Fecha;

                connection.Open();
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var cancha = new Reserva
                    {
                        Id = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                        NombreCliente = reader["NOMBRE_CLIENTE"] != DBNull.Value ? reader["NOMBRE_CLIENTE"].ToString() : string.Empty,
                        Fecha = reader["FECHA"] != DBNull.Value ? Convert.ToDateTime(reader["FECHA"]) : (DateTime?)null,
                        HoraInicio = reader["HORA_INICIO"] != DBNull.Value ? (TimeSpan?)reader["HORA_INICIO"] : null,
                        HoraFin = reader["HORA_FIN"] != DBNull.Value ? (TimeSpan?)reader["HORA_FIN"] : null,
                        CanchaId = reader["CANCHA_ID"] != DBNull.Value ? Convert.ToInt32(reader["CANCHA_ID"]) : 0,
                        MontoTotal = reader["MONTO_TOTAL"] != DBNull.Value ? (decimal?)reader["MONTO_TOTAL"] : 0,
                        MontoPagado = reader["MONTO_PAGADO"] != DBNull.Value ? (decimal?)reader["MONTO_PAGADO"] : 0,
                        TipoPago = reader["TIPO_PAGO"] != DBNull.Value ? reader["TIPO_PAGO"].ToString() : string.Empty,
                    };
                    reservas.Add(cancha);           
                }
                return reservas;
            }
            catch (Exception ex)
            {

                throw new Exception("Error: ", ex);
            }
        }
        public Reserva ObtenerReservaPorId(int Id)
        {
            try
            {

                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_RESERVA_SELECT_BUSCAR_POR_ID", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@ID", SqlDbType.Int, 4).Value = Id;

                connection.Open();
                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Reserva
                    {
                        Id = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                        NombreCliente = reader["NOMBRE_CLIENTE"] != DBNull.Value ? reader["NOMBRE_CLIENTE"].ToString() : string.Empty,
                        Fecha = reader["FECHA"] != DBNull.Value ? Convert.ToDateTime(reader["FECHA"]) : (DateTime?)null,
                        HoraInicio = reader["HORA_INICIO"] != DBNull.Value ? (TimeSpan?)reader["HORA_INICIO"] : null,
                        HoraFin = reader["HORA_FIN"] != DBNull.Value ? (TimeSpan?)reader["HORA_FIN"] : null,
                        MetodoPagoId = reader["METODO_PAGO_ID"] != DBNull.Value ? Convert.ToInt32(reader["METODO_PAGO_ID"]) : 0,
                        CanchaId = reader["CANCHA_ID"] != DBNull.Value ? Convert.ToInt32(reader["CANCHA_ID"]) : 0,
                        MontoTotal = reader["MONTO_TOTAL"] != DBNull.Value ? (decimal?)reader["MONTO_TOTAL"] : 0,
                        MontoPagado = reader["MONTO_PAGADO"] != DBNull.Value ? (decimal?)reader["MONTO_PAGADO"] : 0,
                        Telefono = reader["TELEFONO"] != DBNull.Value ? reader["TELEFONO"].ToString() : string.Empty,
                        TipoPago = reader["TIPO_PAGO"] != DBNull.Value ? reader["TIPO_PAGO"].ToString() : string.Empty,
                    };
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("Error: ", ex);
            }
        }
    }
}
