﻿using mvc.BusinessLogic.BLUsuario;
using mvc.Entities.BaseEntities.UsuarioEntities;
using SR.Entities.BaseEntities.UsuarioEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvc.ServiceClient.SCUsuario
{
    public class UsuarioClient : IUsuarioClient
    {
        private readonly IUsuarioService _usuarioservice;

        public UsuarioClient(IUsuarioService usuarioservice)
        {
            _usuarioservice = usuarioservice;
        }

        public Usuario ValidarLogin(string correo, string contrasenia)
        {
            return _usuarioservice.ValidarLogin(correo, contrasenia);
        }


    }
}
