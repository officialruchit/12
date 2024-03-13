using System;
using System.Globalization;

namespace Tax_calculator
{
    public class Utility
    {
        public bool IsValidName(string name, out string errorCode)
        {
            errorCode = null;
            if (string.IsNullOrWhiteSpace(name))
            {
                errorCode = "E02";
                return false;
            }
            if (name.Length > 50)
            {
                errorCode = "E01";
                return false;
            }
            if (!IsAlpha(name))
            {
                errorCode = "E03";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        public bool IsValidDOB(string dob, out int age, out string errorCode)
        {
            errorCode = null;
            if (string.IsNullOrWhiteSpace(dob))
            {
                errorCode = "E06";
                age = 0;
                return false;
            }
            if (!DateTime.TryParseExact(dob, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth) || dateOfBirth.Year < 1900 || dateOfBirth.Year > 2010)
            {
                errorCode = "E04";
                age = 0;
                return false;
            }
            // Calculate age
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age -= 1;
            if (age < 0)
            {
                errorCode = "E05";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        public bool IsValidGender(char gender, out string errorCode)
        {
            errorCode = null;
            if (char.ToUpper(gender) != 'M' && char.ToUpper(gender) != 'F')
            {
                errorCode = "E08";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        public bool IsValidIncome(string incomeStr, out decimal income, out string errorCode)
        {
            errorCode = null;
            if (string.IsNullOrWhiteSpace(incomeStr))
            {
                errorCode = "E09";
                income = 0;
                return false;
            }
            if (!decimal.TryParse(incomeStr, out income) || income < 0)
            {
                errorCode = "E07";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        public bool IsValidInvestment(string investmentStr, decimal totalIncome, out decimal investment, out string errorCode)
        {
            errorCode = null;
            if (string.IsNullOrWhiteSpace(investmentStr))
            {
                errorCode = "E10";
                investment = 0;
                return false;
            }
            if (!decimal.TryParse(investmentStr, out investment) || investment < 0 || investment > totalIncome)
            {
                errorCode = "E07";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        public bool IsValidHomeLoan(string homeLoanStr, decimal totalIncome, decimal totalInvestment, out decimal homeLoan, out string errorCode)
        {
            errorCode = null;
            if (string.IsNullOrWhiteSpace(homeLoanStr))
            {
                errorCode = "E11";
                homeLoan = 0;
                return false;
            }
            if (!decimal.TryParse(homeLoanStr, out homeLoan) || homeLoan < 0 || homeLoan + totalInvestment > totalIncome)
            {
                errorCode = "E07";
                return false;
            }
            // Additional validation logic if needed
            return true;
        }

        private bool IsAlpha(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                    return false;
            }
            return true;
        }
    }
}
