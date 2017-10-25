using Domain.Entities;
using Infraestructura.Data.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class SampleArchContext : DbContextBase
    {
            public SampleArchContext()
                : base("Name=SampleArchContext")
            {

            }

            public DbSet<Person> Persons { get; set; }
            public DbSet<Country> Countries { get; set; }
    }
}
