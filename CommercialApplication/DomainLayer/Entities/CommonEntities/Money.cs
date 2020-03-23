using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplicationCommand.DomainLayer.Entities.CommonEntities
{
    public class Money : Entity
    {
        public double Value { get; set; }
        public Currency Currency { get; set; }

        public Money(double Value, Currency Currency)
        {
            this.Value = Value;
            this.Currency = Currency;
        }
    }
}
