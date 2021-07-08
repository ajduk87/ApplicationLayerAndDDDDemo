using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialApplication.DomainLayer.Entities.ValueObjects.Common
{
    public class CreationDate : ValueObject
    {
        public string Content;

        public CreationDate(string Content)
        {
            this.Content = Content;
        }

        public static explicit operator CreationDate(DateTime creationDate)
        {
            return new CreationDate(creationDate.ToString("yyyy-MM-dd"));
        }

        public static explicit operator CreationDate(string creationDate)
        {
            return new CreationDate(creationDate);
        }

        public static implicit operator string(CreationDate creationDate)
        {
            return creationDate.Content;
        }
    }
}
