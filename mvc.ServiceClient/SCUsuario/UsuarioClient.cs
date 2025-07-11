﻿using Azure;
using SR.BusinessLogic.BLUsuario;
using SR.Entities.BaseEntities.UsuarioEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.ServiceClient.SCUsuario
{
    public class UsuarioClient : IUsuarioClient
    {
        private readonly IUsuarioService _usuarioservice;

        public UsuarioClient(IUsuarioService usuarioservice)
        {
            _usuarioservice = usuarioservice;
        }
        public ObservableCollection<Usuario> ObtenerListaUsuario(int page, int pageSize, string buscar)
        {
            try
            {
                return _usuarioservice.ObtenerListaUsuario(page, pageSize, buscar);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public Usuario ObtenerUsuarioPorId(int Id)
        {
            try
            {
                return _usuarioservice.ObtenerUsuarioPorId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public bool SaveUsuario(Usuario usuario)
        {
            try
            {
                return _usuarioservice.SaveUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public Usuario ValidarLogin(string correo, string contrasenia)
        {
            return _usuarioservice.ValidarLogin(correo, contrasenia);
        }

        public bool ValidarUsuarioCorreo(string correo, int Id)
        {
            try
            {
                return _usuarioservice.ValidarUsuarioCorreo(correo,Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
        public bool EliminarUsuarioPorId(int Id)
        {
            try
            {
                return _usuarioservice.EliminarUsuarioPorId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
