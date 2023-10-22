using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personality
{


    public class Person
    {
        public string Name { get; set; }
        public decimal InitialBalance { get; set; }
        public string pass { get { return ""; } set { pass = value; } }
        public Person(string Name, decimal InialBalance)
        {
            this.Name = Name;
            this.InitialBalance = InialBalance;
        }
        public static string GetPersonInfo(Person person)
        {
            return $"Name: {person.Name}, Initial balance: {person.InitialBalance}";
        }
    }
}
