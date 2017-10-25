using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class AlwaysCreateInitializer : DropCreateDatabaseAlways<SampleArchContext>
    {
        protected override void Seed(SampleArchContext context)
        {
            var listCountry = new List<Country>() {
               new Country() { Id = 1, Name = "US" },
               new Country() { Id = 2, Name = "India" },
               new Country() { Id = 3, Name = "Russia" }
            };

            context.Countries.AddRange(listCountry);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
