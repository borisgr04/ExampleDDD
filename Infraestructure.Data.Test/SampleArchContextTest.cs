using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data.Test
{
    class SampleArchContextTest: SampleArchContext
    {
        public SampleArchContextTest()
          : base()
        {

        }
        public SampleArchContextTest(DbConnection connection)
          : base(connection)
        {
        
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Suppress code first model migration check          
            Database.SetInitializer<SampleArchContextTest>(new AlwaysCreateInitializer());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
