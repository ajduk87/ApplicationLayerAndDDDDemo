using CommercialApplicationCommand.DomainLayer.Entities.ActionEntities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommercialApplicationCommand.DomainLayer.Entities.ValueObjects.Common;

namespace CommercialApplication.DomainLayer.Extensions
{
    public static class ActionExtensionMethod
    {
        public static Action SingleOrDefault(this IEnumerable<Action> source)
        {
            var action = Enumerable.SingleOrDefault(source);

            if (action == null)
                return new Action { Discount = new Discount(0.0) };

            return action;
        }
    }
}
