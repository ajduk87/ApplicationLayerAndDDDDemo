using CommercialClientApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using CommercialClientApplication.Dtoes;
using CommercialClientApplication.DataGridModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CommercialClientApplication.Urls;

namespace CommercialClientApplication
{
    /// <summary>
    /// Interaction logic for InvoiceControl.xaml
    /// </summary>
    public partial class InvoiceControl : UserControl
    {
        private readonly RegistrationServices registrationServices = new RegistrationServices();
        private readonly IInvoiceService invoiceService;

        private ObservableCollection<OrderItem> OrderInfoItems;
        public ICollectionView cvOrderInfoItems;

        private readonly IApiCaller apiCaller;

        private readonly InvoiceUrls urls;

        public InvoiceControl()
        {
            InitializeComponent();

            this.invoiceService = registrationServices.Container.Resolve<IInvoiceService>();
            this.apiCaller = registrationServices.Container.Resolve<IApiCaller>();
            this.urls = new InvoiceUrls();

            this.OrderInfoItems = new ObservableCollection<OrderItem>();

            cvOrderInfoItems = CollectionViewSource.GetDefaultView(OrderInfoItems);
            if (cvOrderInfoItems != null)
            {
                dgOrderinfo.ItemsSource = cvOrderInfoItems;
            }
        }

        private void BtnGetOrder_Click(object sender, RoutedEventArgs e)
        {
            string responseMessage = this.apiCaller.Get(this.urls.Order, Convert.ToInt32(tfgetorderid.Text));
            string response = Regex.Unescape(responseMessage).Trim('"');
            OrderInfoDto orderDto = JsonConvert.DeserializeObject<OrderInfoDto>(response);

            tfgetcustomername.Text = orderDto.CustomerName;
            this.OrderInfoItems = orderDto.OrderItems;
            dgOrderinfo.ItemsSource = this.OrderInfoItems;
        }

        private void BtnMakeInvoice_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
