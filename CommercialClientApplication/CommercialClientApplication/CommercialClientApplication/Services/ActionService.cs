using CommercialClientApplication.Dtoes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Services
{
    public class ActionService : IActionService
    {
        public void Insert(ActionDto action)
        {
        }
        public void Update(ActionDto action)
        {
        }
        public ActionDto Get(string productName)
        {
            return new ActionDto();
        }
    }
}
