namespace Tax_calculator
{
    public class Tax_calculator : InvestmentInfo
    {
        public Tax_calculator(decimal income, decimal ValidInvestment, decimal HomeLoanOrHouseRentExemption,
            int slab2TaxRate, int slab3TaxRate, int slab4TaxRate) : base(income, ValidInvestment, HomeLoanOrHouseRentExemption)
        {
        }

        public decimal NonTaxableAmount { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TotalTax { get; set; }

        public decimal Slab1Amount { get; set; }
        public decimal Slab2Amount { get; set; }
        public decimal Slab3Amount { get; set; }

        public int Slab2TaxRate { get; set; }
        public int Slab3TaxRate { get; set; }
        public int Slab4TaxRate { get; set; }

        // Method to calculate tax details
        public string CalculateTaxDetails(PersonalInfo personalInfo, InvestmentInfo investmentInfo)
        {
            TaxableAmount = CalcTaxableAmount(investmentInfo, out decimal nonTaxableAmount);
            CalcTotalTax(TaxableAmount);
            return "Success"; // Assuming no error occurred
        }

        // Internal method to calculate taxable amount
        private decimal CalcTaxableAmount(InvestmentInfo investmentInfo, out decimal nonTaxableAmount)
        {
            // Calculate non-taxable amount as 80% of home loan exemption
            decimal homeLoanExemption = 0.8m * investmentInfo.HomeLoanOrHouseRentExemption;

            // If valid investment is less than or equal to 100,000, add it to the non-taxable amount
            if (investmentInfo.ValidInvestment <= 100000)
            {
                nonTaxableAmount = homeLoanExemption + investmentInfo.ValidInvestment;
            }
            else
            {
                nonTaxableAmount = homeLoanExemption + 100000;
            }

            // Assuming starting with 0 taxable amount
            decimal taxableAmount = 0;

            // Calculate taxable amount
            taxableAmount = investmentInfo.income - nonTaxableAmount;

            return taxableAmount;
        }

        private decimal CalcTotalTax(decimal taxableAmount)
        {
            decimal totalTax = 0;

            // Tax slabs for men
            decimal[,] menTaxSlabs = {
        {160000, 0},        // No tax up to 1,60,000
        {300000, 0.10m},    // 10% tax for income between 1,60,001 to 3,00,000
        {500000, 0.20m},    // 20% tax for income between 3,00,001 to 5,00,000
        {decimal.MaxValue, 0.30m}   // 30% tax for income above 5,00,001
    };

            // Tax slabs for women
            decimal[,] womenTaxSlabs = {
        {190000, 0},        // No tax up to 1,90,000
        {300000, 0.10m},    // 10% tax for income between 1,90,001 to 3,00,000
        {500000, 0.20m},    // 20% tax for income between 3,00,001 to 5,00,000
        {decimal.MaxValue, 0.30m}   // 30% tax for income above 5,00,001
    };

            // Tax slabs for senior citizens
            decimal[,] seniorCitizenTaxSlabs = {
        {240000, 0},        // No tax up to 2,40,000 for senior citizens
        {300000, 0.10m},    // 10% tax for income between 2,40,001 to 3,00,000
        {500000, 0.20m},    // 20% tax for income between 3,00,001 to 5,00,000
        {decimal.MaxValue, 0.30m}   // 30% tax for income above 5,00,001
    };

            // Check the taxable amount against each slab and calculate tax
            if (taxableAmount <= menTaxSlabs[0, 0])
            {
                totalTax = taxableAmount * menTaxSlabs[0, 1]; // No tax up to a certain limit
            }
            else if (taxableAmount <= menTaxSlabs[1, 0])
            {
                totalTax = menTaxSlabs[0, 0] * menTaxSlabs[0, 1] + (taxableAmount - menTaxSlabs[0, 0]) * menTaxSlabs[1, 1]; // Tax for the first slab
            }
            else if (taxableAmount <= menTaxSlabs[2, 0])
            {
                totalTax = menTaxSlabs[0, 0] * menTaxSlabs[0, 1] + (menTaxSlabs[1, 0] - menTaxSlabs[0, 0]) * menTaxSlabs[1, 1] + (taxableAmount - menTaxSlabs[1, 0]) * menTaxSlabs[2, 1]; // Tax for the second slab
            }
            else
            {
                totalTax = menTaxSlabs[0, 0] * menTaxSlabs[0, 1] + (menTaxSlabs[1, 0] - menTaxSlabs[0, 0]) * menTaxSlabs[1, 1] + (menTaxSlabs[2, 0] - menTaxSlabs[1, 0]) * menTaxSlabs[2, 1] + (taxableAmount - menTaxSlabs[2, 0]) * menTaxSlabs[3, 1]; // Tax for the third slab
            }

            return totalTax;
        }


        /*  public string CalculateTaxDetails()*/

    }
}








