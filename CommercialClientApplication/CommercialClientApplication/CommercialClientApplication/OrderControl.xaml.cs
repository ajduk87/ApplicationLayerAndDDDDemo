using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using CommercialClientApplication.DataGridModels;
using CommercialClientApplication.Dtoes;
using CommercialClientApplication.Services;
using CommercialClientApplication.Urls;
using Newtonsoft.Json;

namespace CommercialClientApplication
{
    /// <summary>
    /// Interaction logic for OrderControl.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {
        private readonly RegistrationServices registrationServices = new RegistrationServices();
        private readonly IOrderService orderService;

        private readonly OrderUrls urls;
        private IApiCaller apiCaller;

        private readonly ObservableCollection<OrderItemDto> OrderItemDtoes;
        private ObservableCollection<OrderItem> OrderItems;
        private ObservableCollection<OrderItem> OrderInfoItems;
        public ICollectionView cvOrderItems;
        public ICollectionView cvOrderInfoItems;

        public OrderControl()
        {
            InitializeComponent();

            this.orderService = registrationServices.Container.Resolve<IOrderService>();

            this.urls = new OrderUrls();

            this.apiCaller = registrationServices.Container.Resolve<IApiCaller>();
            this.OrderItemDtoes = new ObservableCollection<OrderItemDto>();
            this.OrderItems = new ObservableCollection<OrderItem>();
            this.OrderInfoItems = new ObservableCollection<OrderItem>();

            cvOrderItems = CollectionViewSource.GetDefaultView(OrderItems);
            if (cvOrderItems != null)
            {
                dgCurrentOrder.ItemsSource = cvOrderItems;
            }


        }

        private void BtnFinishOrder_Click(object sender, RoutedEventArgs e)
        {
            string responseMessage = this.apiCaller.Get(this.urls.CustomerByName, new object[] { tfentercustomername.Text });
            string response = Regex.Unescape(responseMessage).Trim('"');
            long customerId = JsonConvert.DeserializeObject<CustomerDto>(response).Id;

            OrderDto orderDto = new OrderDto
            {
                CustomerId = customerId,
                OrderItems = this.OrderItemDtoes
            };

            this.apiCaller.Post(this.urls.Order, orderDto);

            this.OrderItemDtoes.Clear();
        }

        private void BtnGetOrderInfo_Click(object sender, RoutedEventArgs e)
        {
            string responseMessage = this.apiCaller.Get(this.urls.Order, Convert.ToInt32(tfgetorderid.Text));
            string response = Regex.Unescape(responseMessage).Trim('"');
            OrderInfoDto orderDto = JsonConvert.DeserializeObject<OrderInfoDto>(response);

            tfgetcustomername.Text = orderDto.CustomerName;
            this.OrderInfoItems = orderDto.OrderItems;
            dgOrderinfo.ItemsSource = this.OrderInfoItems;

        }

        private void BtnEnterOrderItem_Click(object sender, RoutedEventArgs e)
        {
            string responseMessage = this.apiCaller.Get(this.urls.Product, new object[] { tfenterproductname.Text });
            string response = Regex.Unescape(responseMessage).Trim('"');
            ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(response);

            responseMessage = this.apiCaller.Get(this.urls.ActionByProductId, new object[] { productDto.Id });
            response = Regex.Unescape(responseMessage).Trim('"');
            ActionDto actionDto = JsonConvert.DeserializeObject<ActionDto>(response);

            OrderItemDto orderItemDto = new OrderItemDto
            {
                ProductId = productDto.Id,
                Amount = Convert.ToInt32(tfenteramount.Text),
                DiscountBasic = Convert.ToDouble(tfenterbasicdiscount.Text),
                ActionId = actionDto.Id
            };

            this.OrderItemDtoes.Add(orderItemDto);

            OrderItem orderItem = new OrderItem
            {
                ProductName = tfenterproductname.Text,
                Amount = Convert.ToInt32(tfenteramount.Text),
                Value = $"{ (1 - Convert.ToDouble(tfenterbasicdiscount.Text)) * Convert.ToInt32(tfenteramount.Text) * productDto.UnitCost.Value } {productDto.UnitCost.Currency}"
            };

            this.OrderItems.Add(orderItem);

            dgCurrentOrder.ItemsSource = this.OrderItems;

        }
    }
}
