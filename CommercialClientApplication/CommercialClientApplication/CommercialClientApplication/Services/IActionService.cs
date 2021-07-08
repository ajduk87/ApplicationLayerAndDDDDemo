using CommercialClientApplication.Dtoes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Services
{
    public interface IActionService
    {
        void Insert(ActionDto action);
        void Update(ActionDto action);
        ActionDto Get(string productName);
    }
}
