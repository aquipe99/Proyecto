﻿using Azure;
using SR.BusinessLogic.BLCancha;
using SR.Entities.BaseEntities.CanchaEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SR.ServiceClient.SCCancha
{
    public class CanchaClient : ICanchaClient
    {
        private readonly ICanchaService _canchaService;

        public CanchaClient(ICanchaService canchaService)
        {
            _canchaService = canchaService;
        }

        public bool EliminarCanchaPorId(int Id)
        {
            try
            {
                return _canchaService.EliminarCanchaPorId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public bool GuardarCancha(Cancha cancha)
        {
            try
            {
                return _canchaService.GuardarCancha(cancha);
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
                return _canchaService.ObtenerCanchaPorId(Id);
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
                return _canchaService.ObtenerListaCanchas(page, pageSize,buscar);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public ObservableCollection<Cancha> ObtenerTodasLasCanchas()
        {
            try
            {
                return _canchaService.ObtenerTodasLasCanchas();
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
                return _canchaService.ValidarCanchaNombre(nombre,Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
