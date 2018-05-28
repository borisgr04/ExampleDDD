using Application.Base;
using Application.Contracts;
using Domain.Abstracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implements
{

    public class CrearCuentaBancariaService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly ICuentaBancariaRepository _cuentaBancariaRepository;

        public CrearCuentaBancariaService(IUnitOfWork unitOfWork, ICuentaBancariaRepository cuentaBancariaRepository)
        {
            _unitOfWork = unitOfWork;
            _cuentaBancariaRepository = cuentaBancariaRepository;
        }
        public CrearCuentaBancariaResponse Ejecutar(CrearCuentaBancariaRequest request)
        {
            CuentaBancaria cuenta = _cuentaBancariaRepository.FindBy(t => t.Numero.Equals(request.Numero)).FirstOrDefault();
            if (cuenta == null)
            {
                CuentaBancaria cuentaNueva = new CuentaAhorro();//Debe ir un factory que determine que tipo de cuenta se va a crear
                cuentaNueva.Nombre = request.Nombre;
                cuentaNueva.Numero = request.Numero;
                _cuentaBancariaRepository.Add(cuentaNueva);
                _unitOfWork.Commit();
                return new CrearCuentaBancariaResponse() { Mensaje = $"Se creo con exito la cuenta  {cuentaNueva.Numero}." };
            }
            else
            {
                return new CrearCuentaBancariaResponse() { Mensaje = $"El número de cuenta ya exite" };
            }
        }

      

    }
    public class CrearCuentaBancariaRequest
    {
        public string Nombre { get; set; }
        public string TipoCuenta { get; set; }
        public string Numero { get; set; }
    }
    public class CrearCuentaBancariaResponse
    {
        public string Mensaje { get; set; }
    }
}