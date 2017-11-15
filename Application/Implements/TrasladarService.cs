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
    public class TrasladarService: IOperacionFinanciera
    {
        readonly IUnitOfWork _unitOfWork;
        readonly ICuentaBancariaRepository _cuentaBancariaRepository;

        public TrasladarService(IUnitOfWork unitOfWork, ICuentaBancariaRepository cuentaBancariaRepository)
        {
            _unitOfWork = unitOfWork;
            _cuentaBancariaRepository = cuentaBancariaRepository;
        }
        public TrasladarResponse Ejecutar(TrasladarRequest request) {
            CuentaBancaria cuentaOrigen = _cuentaBancariaRepository.FindBy(t => t.Numero.Equals(request.NumeroCuenta)).FirstOrDefault();
            if (cuentaOrigen == null)
            {
                return new TrasladarResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
            CuentaBancaria cuentaDestino = _cuentaBancariaRepository.FindBy(t => t.Numero.Equals(request.NumeroCuentaDestino)).FirstOrDefault();
            if (cuentaDestino == null)
            {
                return new TrasladarResponse() { Mensaje = $"Número de Cuenta No Válido." };
            }
            cuentaOrigen.Trasladar(cuentaDestino,request.Valor);
            _cuentaBancariaRepository.Edit(cuentaOrigen);
            _cuentaBancariaRepository.Edit(cuentaDestino);
            _unitOfWork.Commit();
            return new TrasladarResponse() { Mensaje = $"Su nuevo saldo es {cuentaOrigen.Saldo}." };

        }
    }
    public class TrasladarRequest
    {
        public string NumeroCuenta { get; set; }
        public string NumeroCuentaDestino { get; set; }
        public double Valor { get; set; }
    }
    public class TrasladarResponse
    {
        public string Mensaje { get; set; }
    }
}
