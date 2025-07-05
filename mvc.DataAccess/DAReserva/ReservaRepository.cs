using Microsoft.Data.SqlClient;
using SR.DataAccess.Servicios;
using SR.Entities.BaseEntities.CanchaEntities;
using SR.Entities.BaseEntities.ReservaEntities;
using System;
using System.Collections.Generic;
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
                int resultado = (int)resultadoParam.Value;
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

    }
}
