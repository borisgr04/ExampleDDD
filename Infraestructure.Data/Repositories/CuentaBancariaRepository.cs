using Domain.Abstracts;
using Domain.Entities;
using Infraestructura.Data.Base;
using Infraestructure.Data.Repositories;
using SirccELC.Infraestructura.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data.Repositories
{
    public class CuentaBancariaRepository : GenericRepository<CuentaBancaria>, ICuentaBancariaRepository
    {
        public CuentaBancariaRepository(IDbContext context)
              : base(context)
        {

        }

    }
}
