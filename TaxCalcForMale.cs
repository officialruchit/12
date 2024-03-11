using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tax_calculator
{
    public class TaxCalcForMale : Tax_calculator
    {
        // Constructor
        public TaxCalcForMale(decimal slab1Amount, decimal slab2Amount, decimal slab3Amount, int slab2TaxRate, int slab3TaxRate, int slab4TaxRate)
            : base(slab1Amount, slab2Amount, slab3Amount, slab2TaxRate, slab3TaxRate, slab4TaxRate) { }
    }
}
