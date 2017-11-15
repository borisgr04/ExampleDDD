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
    public class RetirarService: IOperacionFinanciera
    {
        readonly IUnitOfWork _unitOfWork;
        readonly ICuentaBancariaRepository _cuentaBancariaRepository;

        public RetirarService(IUnitOfWork unitOfWork, ICuentaBancariaRepository cuentaBancariaRepository)
        {
            _unitOfWork = unitOfWork;
            _cuentaBancariaRepository = cuentaBancariaRepository;
        }
        public RetirarResponse Ejecutar(RetirarRequest request) {
            CuentaBancaria cuenta = _cuentaBancariaRepository.FindBy(t => t.Numero.Equals(request.NumeroCuenta)).FirstOrDefault();
            if (cuenta != null)
            {
                cuenta.Retirar(request.Valor);
                _cuentaBancariaRepository.Edit(cuenta);
                _unitOfWork.Commit();
                return new RetirarResponse() { Mensaje = $"Su nuevo saldo es {cuenta.Saldo}." };
            }
            else {
                return new RetirarResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
        }
    }
    public class RetirarRequest
    {
        public string NumeroCuenta { get; set; }
        public double Valor { get; set; }
    }
    public class RetirarResponse
    {
        public string Mensaje { get; set; }
    }
}
