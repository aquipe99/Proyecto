﻿using SR.Entities.BaseEntities.MenuEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.ServiceClient.SCMenu
{
    public interface IMenuClient
    {
       ObservableCollection<Menu> ObtenerMenuPorusuario(int id);
        ObservableCollection<Menu> ObtenerListaMenu();
    }
}
