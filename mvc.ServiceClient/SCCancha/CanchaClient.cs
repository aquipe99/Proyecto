using Azure;
using SR.BusinessLogic.BLCancha;
using SR.Entities.BaseEntities.CanchaEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.ServiceClient.SCCancha
{
    public class CanchaClient : ICanchaClient
    {
        private readonly ICanchaService _canchaService;

        public CanchaClient(ICanchaService canchaService)
        {
            _canchaService = canchaService;
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

        public ObservableCollection<Cancha> ObtenerListaCanchas(int page, int pageSize)
        {
            try
            {
                return _canchaService.ObtenerListaCanchas(page, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public bool ValidarCanchaNombre(string nombre)
        {
            try
            {
                return _canchaService.ValidarCanchaNombre(nombre);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
