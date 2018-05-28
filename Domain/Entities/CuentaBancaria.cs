using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class CuentaBancaria: Entity<int>,IServicioFinanciero
    {
        protected CuentaBancaria()
        {
            Movimientos = new List<MovimientoFinanciero>();
        }

        public List <MovimientoFinanciero> Movimientos { get; set; }
        public  string Nombre { get; set; }
        public  string Numero { get; set; }

        public double Saldo{get; protected set;}
       
        
        public  virtual void Consignar(double valor)
        {
            MovimientoFinanciero movimiento = new MovimientoFinanciero();
            movimiento.ValorConsignacion = valor;
            movimiento.FechaMovimiento = DateTime.Now;
            Saldo += valor;
            Movimientos.Add(movimiento);
            
        }
        public abstract  void Retirar(double valor);

        public override string ToString()
        {
            return ($"Su saldo disponible es {Saldo}.");
        }

        public void Trasladar(IServicioFinanciero servicioFinanciero, double valor)
        {
            Retirar(valor);
            servicioFinanciero.Consignar(valor);
        }
    }
}
