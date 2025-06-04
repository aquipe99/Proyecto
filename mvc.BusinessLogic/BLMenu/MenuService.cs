using SR.DataAccess.DAMenu;
using SR.Entities.BaseEntities.MenuEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.BusinessLogic.BLMenu
{
    public class MenuService:IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository) { 
            _menuRepository = menuRepository;
        }

        public ObservableCollection<Menu> ObtenerMenuPorusuario(int id)
        {
            try {
                return _menuRepository.ObtenerMenuPorusuario(id);
            }
            catch (Exception ex ) {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
