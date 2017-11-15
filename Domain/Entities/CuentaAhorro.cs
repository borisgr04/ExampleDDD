using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CuentaAhorro : CuentaBancaria
    {
        public const double TOPERETIRO= 1000;

       
        public override void Retirar(double valor)
        {
            double nuevoSaldo = Saldo - valor;
            if (nuevoSaldo > TOPERETIRO)
            {
                MovimientoFinanciero retiro = new MovimientoFinanciero();
                retiro.ValorRetiro = valor;
                retiro.FechaMovimiento = DateTime.Now;
                Saldo -= valor;
                this.Movimientos.Add(retiro);
            }
            else
            {
                throw new CuentaAhorroTopeDeRetiroException("No es posible realizar el Retiro, Supera el tope mínimo permitido de retiro");
            }
        }
    }


    [Serializable]
    public class CuentaAhorroTopeDeRetiroException : Exception
    {
        public CuentaAhorroTopeDeRetiroException() { }
        public CuentaAhorroTopeDeRetiroException(string message) : base(message) { }
        public CuentaAhorroTopeDeRetiroException(string message, Exception inner) : base(message, inner) { }
        protected CuentaAhorroTopeDeRetiroException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
