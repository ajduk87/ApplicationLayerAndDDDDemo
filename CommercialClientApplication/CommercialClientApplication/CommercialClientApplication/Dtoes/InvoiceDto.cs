using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommercialClientApplication.Dtoes
{
    public class InvoiceDto
    {
        public string CustomerName { get; set; }
        public ObservableCollection<InvoiceItemDto> InvoiceItems { get; set; }
    }
}
