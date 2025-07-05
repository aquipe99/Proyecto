using Azure;
using SR.DataAccess.DACancha;
using SR.Entities.BaseEntities.CanchaEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SR.BusinessLogic.BLCancha
{
    public class CanchaService : ICanchaService
    {
        private readonly ICanchaRepository _canchaRepository;

        public CanchaService(ICanchaRepository canchaRepository)
        {
            _canchaRepository = canchaRepository;
        }

        public bool EliminarCanchaPorId(int Id)
        {
            try
            {
                return _canchaRepository.EliminarCanchaPorId(Id);
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
                return _canchaRepository.GuardarCancha(cancha);
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
                return _canchaRepository.ObtenerCanchaPorId(Id);
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
                return _canchaRepository.ObtenerListaCanchas(page,pageSize,buscar);
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
                return _canchaRepository.ObtenerTodasLasCanchas();
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
                return _canchaRepository.ValidarCanchaNombre(nombre,Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }
    }
}
