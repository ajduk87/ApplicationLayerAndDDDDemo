using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.DomainLayer.Entities.ValueObjects.Common
{
    public class Money : ValueObject
    {
        public double Value { get; }
        public Currency Currency { get; }

        private bool Validate(double Value)
        {
            return Value >= 0;
        }

        public Money(double Value, Currency Currency)
        {
            if (!Validate(Value)) throw new Exception("Amount of money must not be negative.");
            //if (Validate(Value) == false) throw new Exception("Amount of money must not be negative.");
            this.Value = Value;
            this.Currency = Currency;
        }
    }
}
