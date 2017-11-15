using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class MovimientoFinanciero: Entity<int>
    {
        public CuentaBancaria CuentaBancaria { get; set; }
        public double ValorRetiro { get; set; }
        public double ValorConsignacion { get; set; }
        public DateTime FechaMovimiento { get; set; }
   }
}
