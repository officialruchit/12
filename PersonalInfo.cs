using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tax_calculator
{
    public class PersonalInfo
    {
        public string PersonName { get; set; }
        public string DOB { get; set; }
        public char Gender { get; set; }

        // Constructor
        public PersonalInfo(string PersonName, string DOB, char Gender)
        {
            this.PersonName = PersonName;
            this.DOB = DOB;
            this.Gender = Gender;
        }
    }
}
