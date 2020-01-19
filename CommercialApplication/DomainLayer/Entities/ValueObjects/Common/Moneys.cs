using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.DomainLayer.Entities.ValueObjects.Common
{
    public class Moneys : ValueObject
    {
        public double ValueInEuros { get; }
        public Currency CurrencyEuro { get; }

        public double ValueInDollars { get; }
        public Currency CurrencyDollar { get; }

        public double ValueInDinars { get; }
        public Currency CurrencyDinar { get; }

        private bool Validate(double ValueInEuros, double ValueInDollars, double ValueInDinars)
        {
            return ValueInEuros >= 0 && ValueInDollars >= 0 && ValueInDinars >= 0;
        }

        public Moneys(double ValueInEuros, double ValueInDollars, double ValueInDinars)
        {
            if (!Validate(ValueInEuros, ValueInDollars, ValueInDinars)) throw new Exception("Amount of money must not be negative.");
            //if (Validate(Value) == false) throw new Exception("Amount of money must not be negative.");
            this.ValueInEuros = ValueInEuros;
            this.ValueInDollars = ValueInDollars;
            this.ValueInDinars = ValueInDinars;
            this.CurrencyEuro = new Currency("eur");
            this.CurrencyDollar = new Currency("dollar");
            this.CurrencyDinar = new Currency("dinar");
        }
    }
}
