using SR.BusinessLogic.BLMenu;
using SR.DataAccess.DAMenu;
using SR.Entities.BaseEntities.MenuEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.ServiceClient.SCMenu
{
    public class MenuClient : IMenuClient
    {
        private readonly IMenuService _menuService;

        public MenuClient(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public ObservableCollection<Menu> ObtenerMenuPorusuario(int id)
        {
            try
            {
                return _menuService.ObtenerMenuPorusuario(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
