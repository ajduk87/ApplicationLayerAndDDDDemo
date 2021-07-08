using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;
using System;

namespace CommercialApplicationCommand.DomainLayer.Entities.ProductEntities
{
    public class UnitCost : Entity
    {
        public double Value { get; set; }
        public Currency Currency { get; set; }

        public UnitCost() { }
        public UnitCost(double Value, Currency Currency)
        {
            this.Value = Value;
            this.Currency = Currency;
        }

        public override string ToString()
        {
            return $"{Value} {Currency.Content}";
        }

        //output
        public static explicit operator UnitCost(string unitOfCost)
        {
            return new UnitCost(Convert.ToDouble(unitOfCost.Split(' ')[0]), new Currency(unitOfCost.Split(' ')[1]));
        }

        //input
        public static implicit operator string(UnitCost unitOfCost)
        {
            return $"{unitOfCost.Value} {unitOfCost.Currency.Content}";
        }
    }
}