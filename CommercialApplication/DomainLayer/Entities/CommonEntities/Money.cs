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

        public Money()
        {

        }

        public Money(decimal val)
        {
            Value = (double)val;
            Currency = new Currency("din");
        }

        public static explicit operator Money(decimal val)
        {
            return new Money(val);
        }
    }
}
