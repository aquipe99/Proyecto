using Azure;
using SR.DataAccess.DACancha;
using SR.DataAccess.DAMetodoPago;
using SR.Entities.BaseEntities.MetodoPagoEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR.BusinessLogic.BLMetodoPago
{
    public class MetodoPagoService :IMetodoPagoService
    {
        private readonly IMetodoPagoRepository _metodoPagoRepository;

        public MetodoPagoService(IMetodoPagoRepository metodoPagoRepository)
        {
            _metodoPagoRepository = metodoPagoRepository;
        }

        public bool EliminarMetodoPagoPorId(int Id)
        {
            try
            {
                return _metodoPagoRepository.EliminarMetodoPagoPorId(Id);
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
                return _metodoPagoRepository.GuardarMetodoPago(metodoPago);
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
                return _metodoPagoRepository.ObtenerListaMetodoPago(page,pageSize,buscar);
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
                return _metodoPagoRepository.ObtenerMetodoPagoPorId(Id);
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
                return _metodoPagoRepository.ValidarMetodoPagoNombre(nombre,Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
