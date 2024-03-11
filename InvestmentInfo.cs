namespace Tax_calculator
{
    public class InvestmentInfo
    {
        public decimal income { get; set; }
        public decimal HomeLoanOrHouseRentExemption { get; set; }
        public decimal ValidInvestment { get;set; }

     public  InvestmentInfo(decimal income, decimal ValidInvestment, decimal HomeLoanOrHouseRentExemption)
        {
            this.income = income;
            this.ValidInvestment = ValidInvestment;
            this.HomeLoanOrHouseRentExemption = HomeLoanOrHouseRentExemption;
        }
    }
}
