using Application.Base;
using Domain.Abstracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implements
{
    public class ConsignarService: IOperacionFinanciera
    {
        readonly IUnitOfWork _unitOfWork;
        readonly ICuentaBancariaRepository _cuentaBancariaRepository;

        public ConsignarService(IUnitOfWork unitOfWork, ICuentaBancariaRepository cuentaBancariaRepository)
        {
            _unitOfWork = unitOfWork;
            _cuentaBancariaRepository = cuentaBancariaRepository;
        }
        public ConsignarResponse Ejecutar(ConsignarRequest request) {
            CuentaBancaria cuenta = _cuentaBancariaRepository.FindBy(t => t.Numero.Equals(request.NumeroCuenta)).FirstOrDefault();
            if (cuenta != null)
            {
                cuenta.Consignar(request.Valor);
                _cuentaBancariaRepository.Edit(cuenta);
                _unitOfWork.Commit();
                return new ConsignarResponse() { Mensaje = $"Su Nuevo saldo es {cuenta.Saldo}." };
            }
            else {
                return new ConsignarResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
        }
    }
    public class ConsignarRequest
    {
        public string NumeroCuenta { get; set; }
        public double Valor { get; set; }
    }
    public class ConsignarResponse
    {
        public string Mensaje { get; set; }
    }
}
