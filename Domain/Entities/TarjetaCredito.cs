using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum TipoTarjetaCredito
    {
        Visa,
        MasterCard,
        DinersClub,
        AmericanExpress
    }
    public class TarjetaCredito : IServicioFinanciero
    {
 
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public double Saldo {get; protected set;}

        public List<MovimientoFinanciero> Movimientos { get; set; }

        private readonly double CupoMaximo;

        public TipoTarjetaCredito Tipo { get; set; }
        
        public TarjetaCredito(string numero, double cupoMaximo, string nombre, TipoTarjetaCredito tipo)
        {
            Movimientos = new List<MovimientoFinanciero>();
            Numero = numero;
            Nombre = nombre;
            CupoMaximo = cupoMaximo;
   
        }
        public void Consignar(double valor)
        {
            MovimientoFinanciero consignacion = new MovimientoFinanciero();
            consignacion.ValorConsignacion = valor;
            consignacion.FechaMovimiento = DateTime.Now;
            Saldo -= valor;
            Movimientos.Add(consignacion);
        }

        public void Retirar(double valor)
        {
            double SaldoTemporal = Saldo + valor;
            if (SaldoTemporal <= CupoMaximo)
            {
                MovimientoFinanciero retiro = new MovimientoFinanciero();
                retiro.ValorRetiro = valor;
                retiro.FechaMovimiento = DateTime.Now;
                Saldo += valor;
                Movimientos.Add(retiro);
            }
            else
            {
                throw new TarjetaCreditoRetiroExcedeCupoException("No se puede retirar, excede el cupo");
            }
            
        }

        public override string ToString()
        {
            return ($"Señor {Nombre} el saldo de su Tarjeta {Tipo} es {Saldo}");
        }
    }


    [Serializable]
    public class TarjetaCreditoRetiroExcedeCupoException : Exception
    {
        public TarjetaCreditoRetiroExcedeCupoException() { }
        public TarjetaCreditoRetiroExcedeCupoException(string message) : base(message) { }
        public TarjetaCreditoRetiroExcedeCupoException(string message, Exception inner) : base(message, inner) { }
        protected TarjetaCreditoRetiroExcedeCupoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
