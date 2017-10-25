using Domain.Entities;
using Infraestructura.Data.Base;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data.Test
{
    class SampleArchContextTest : SampleArchContext
    {
        public SampleArchContextTest(): base()
        {

        }
        public SampleArchContextTest(DbConnection connection)
          : base(connection)
        {
            Log = Console.WriteLine;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Suppress code first model migration check          
            Database.SetInitializer<SampleArchContextTest>(new AlwaysCreateInitializer());


            base.OnModelCreating(modelBuilder);
        }
        public void Seed(SampleArchContext context)
        {
            var listCountry = new List<Country>() {
             new Country() { Id = 1, Name = "US" },
             new Country() { Id = 2, Name = "India" },
             new Country() { Id = 3, Name = "Russia" }
            };
            context.Countries.AddRange(listCountry);
            context.SaveChanges();
        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<SampleArchContextTest>
        {
            protected override void Seed(SampleArchContextTest context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<SampleArchContextTest>
        {
            protected override void Seed(SampleArchContextTest context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class AlwaysCreateInitializer : DropCreateDatabaseAlways<SampleArchContextTest>
        {
            protected override void Seed(SampleArchContextTest context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

    }
}
