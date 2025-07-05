using SR.BusinessLogic.BLMetodoPago;
using SR.DataAccess.DAMetodoPago;
using SR.Entities.BaseEntities.MetodoPagoEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.ServiceClient.SCMetodoPago
{
    public class MetodoPagoClient :IMetodoPagoClient
    {
        private readonly IMetodoPagoService _metodoPagoService;

        public MetodoPagoClient(IMetodoPagoService metodoPagoService)
        {
            _metodoPagoService = metodoPagoService;
        }
        public bool EliminarMetodoPagoPorId(int Id)
        {
            try
            {
                return _metodoPagoService.EliminarMetodoPagoPorId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public bool GuardarMetodoPago(MetodoPago metodoPago)
        {
            try
            {
                return _metodoPagoService.GuardarMetodoPago(metodoPago);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public ObservableCollection<MetodoPago> ObtenerListaMetodoPago(int page, int pageSize, string buscar)
        {
            try
            {
                return _metodoPagoService.ObtenerListaMetodoPago(page, pageSize, buscar);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public MetodoPago ObtenerMetodoPagoPorId(int Id)
        {
            try
            {
                return _metodoPagoService.ObtenerMetodoPagoPorId(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public ObservableCollection<MetodoPago> ObtenerTodosLosMetodosPago()
        {
            try
            {
                return _metodoPagoService.ObtenerTodosLosMetodosPago();
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        public bool ValidarMetodoPagoNombre(string nombre, int Id)
        {
            try
            {
                return _metodoPagoService.ValidarMetodoPagoNombre(nombre, Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
