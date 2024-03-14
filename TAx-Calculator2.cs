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
        public string CalculateTaxDetails(PersonalInfo personalInfo, InvestmentInfo investmentInfo, out Tax_calculator taxInfo)
        {
            Tax_calculator taxInfo = new Tax_calculator(); // Initialize taxInfo (may not be needed depending on your implementation)

            // Initialize error code to empty string
            string errorCode = "";
            Utility utility = new Utility();
            // Check for errors in PersonalInfo
            if (string.IsNullOrWhiteSpace(personalInfo.PersonName))
                errorCode = "E02"; // Input not given for "name"
            else if (!utility.IsValidName(personalInfo.PersonName,out errorCode))
                errorCode = "E03"; // Input contains invalid characters
            else if (!utility.IsValidDOB(personalInfo.DOB,out age,out errorCode))
                errorCode = "E04"; // Date format is invalid
            else if (!IsBirthYearInRange(personalInfo.DOB))
                errorCode = "E05"; // Birth year is out of range
            else if (string.IsNullOrWhiteSpace(personalInfo.DOB))
                errorCode = "E06"; // Input not given for "date of birth"
            else if (personalInfo.Gender != 'M' && personalInfo.Gender != 'F')
                errorCode = "E07"; // Invalid input for "gender"

            // Check for errors in InvestmentInfo
            else if (investmentInfo.income <= 0)
                errorCode = "E09"; // Input not given for "income"
            else if (investmentInfo.ValidInvestment <= 0)
                errorCode = "E10"; // Input not given for "investment"
            else if (investmentInfo.HomeLoanOrHouseRentExemption <= 0)
                errorCode = "E11"; // Input not given for "house loan/rent"
            else if (investmentInfo.ValidInvestment > investmentInfo.income)
                errorCode = "E12"; // Investment cannot be more than income
            else if (investmentInfo.ValidInvestment + investmentInfo.HomeLoanOrHouseRentExemption > investmentInfo.income)
                errorCode = "E13"; // Investment combined with house loan/rent cannot be more than income

            // If no errors found, calculate tax details
            if (string.IsNullOrEmpty(errorCode))
            {
                // Calculate taxable amount
                decimal taxableAmount = CalcTaxableAmount(investmentInfo, out decimal nonTaxableAmount);

                // Calculate total tax
                decimal totalTax = CalcTotalTax(taxableAmount);

                // Return tax details or store them in taxInfo (depending on your implementation)
                return $"Tax details calculated successfully. Total tax: {totalTax}";
            }
            else
            {
                // Return error code
                return errorCode;
            }
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








