using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Dtoes
{
    public class OrderInfoDto
    {
        public string CustomerName { get; set; }
        public ObservableCollection<DataGridModels.OrderItem> OrderItems { get; set; }
    }
}
