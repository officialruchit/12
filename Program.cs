using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tax_calculator
{
    public class Program
    {
        static void Main(string[] args)
        {

        }
        static void RunTest(Utility validator, string name, string dob, object gender, string incomeStr, string investmentStr, string homeLoanStr, string expectedErrorCode, string expectedNonTaxableAmount, string expectedTaxableAmount, string expectedPayableTaxAmount)
        {
            string errorCode;
            int age;
            decimal income, investment, homeLoan;

            if (!validator.IsValidName(name, out errorCode))
                Console.WriteLine($"{errorCode}\t");
            else if (!validator.IsValidDOB(dob, out age, out errorCode))
                Console.WriteLine($"{errorCode}\t");
            else if (!validator.IsValidGender(Convert.ToChar(gender), out errorCode))
                Console.WriteLine($"{errorCode}\t");
            else if (!validator.IsValidIncome(incomeStr, out income, out errorCode))
                Console.WriteLine($"{errorCode}\t");
            else if (!validator.IsValidInvestment(investmentStr, income, out investment, out errorCode))
                Console.WriteLine($"{errorCode}\t");
            else if (!validator.IsValidHomeLoan(homeLoanStr, income, investment, out homeLoan, out errorCode))
                Console.WriteLine($"{errorCode}\t");
            else
                Console.WriteLine($"-\t{expectedNonTaxableAmount}\t{expectedTaxableAmount}\t{expectedPayableTaxAmount}");
        }
    }
}
