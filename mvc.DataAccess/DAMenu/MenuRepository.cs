using Microsoft.Data.SqlClient;
using mvc.DataAccess.Servicios;
using mvc.Entities.BaseEntities.UsuarioEntities;
using SR.Entities.BaseEntities.MenuEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.DataAccess.DAMenu
{
    public class MenuRepository : IMenuRepository
    {
        private readonly string _connectionString;

        public MenuRepository(ConnectionService connectionService) { 
         _connectionString = connectionService.ConnectionString;
        }
        public ObservableCollection<Menu> ObtenerMenuPorusuario(int id)
        {
            try
            {
                var menus = new ObservableCollection<Menu>();
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand("SP_PERMISO_SELECT_OBTENERMENUSPORUSAURIO", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add("@USUARIO_ID", SqlDbType.Int,4).Value = id;

                connection.Open();
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var menu = new Menu
                    {
                        Id = reader["ID"] != DBNull.Value ? Convert.ToInt32(reader["ID"]) : 0,
                        Descipcion = reader["DESCRIPCION"] != DBNull.Value ? reader["DESCRIPCION"].ToString() : string.Empty,
                        Link = reader["LINK"] != DBNull.Value ? reader["LINK"].ToString() : string.Empty,
                        Icono = reader["ICONO"] != DBNull.Value ? reader["ICONO"].ToString() : string.Empty,
                        Menupadre = reader["MENU_PADRE"] != DBNull.Value ? Convert.ToInt32(reader["MENU_PADRE"]) : null,
                        OrdenMenu = reader["ORDEN_MENU"] != DBNull.Value ? Convert.ToInt32(reader["ORDEN_MENU"]) : null,
                    };
                    menus.Add(menu);
                }
                return menus;
            }
            catch (Exception ex) {
                throw new Exception("Error: ", ex);
            }
    
        }
    }
}
