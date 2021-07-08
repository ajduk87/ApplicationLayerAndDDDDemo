using System;
using System.Collections.Generic;
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
using CommercialClientApplication.Dtoes;
using CommercialClientApplication.Services;
using CommercialClientApplication.Urls;
using Newtonsoft.Json;

namespace CommercialClientApplication
{
    /// <summary>
    /// Interaction logic for ActionControl.xaml
    /// </summary>
    public partial class ActionControl : UserControl
    {

        private readonly RegistrationServices registrationServices = new RegistrationServices();
        private readonly IActionService actionService;

        private readonly ActionUrls urls;

        private readonly IApiCaller apiCaller;

        public ActionControl()
        {
            InitializeComponent();


            this.urls = new ActionUrls();

            this.apiCaller = registrationServices.Container.Resolve<IApiCaller>();

            this.actionService = registrationServices.Container.Resolve<IActionService>();
        }


        private void BtnEnterProduct_Click_1(object sender, RoutedEventArgs e)
        {
            string name = tfenterproduct.Text;

            string responseMessage = this.apiCaller.Get(this.urls.Product, new object[] { name });
            string response = Regex.Unescape(responseMessage).Trim('"');
            ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(response);

            ActionDto actionDto = new ActionDto
            {
                ProductId = productDto.Id,
                Discount = Convert.ToDouble(tfenterdiscount.Text),
                ThresholdAmount = Convert.ToInt32(tfenterthresholdamount.Text)
            };

            this.apiCaller.Post(this.urls.Action, actionDto);
        }

        private void BtnUpdateAction_Click(object sender, RoutedEventArgs e)
        {
            string name = tfupdateproduct.Text;

            string responseMessage = this.apiCaller.Get(this.urls.Product, new object[] { name });
            string response = Regex.Unescape(responseMessage).Trim('"');
            ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(response);

            ActionDto actionDto = new ActionDto
            {
                Id = 1,
                ProductId = productDto.Id,
                Discount = Convert.ToDouble(tfupdatediscount.Text),
                ThresholdAmount = Convert.ToInt32(tfupdatethresholdamount.Text),
                CustomerId = 1
            };

            this.apiCaller.Put(this.urls.Action, actionDto);
        }

        private void BtnGetActionInfo_Click(object sender, RoutedEventArgs e)
        {
            string name = tfgetproductname.Text;

            string responseMessage = this.apiCaller.Get(this.urls.Product, new object[] { name });
            string response = Regex.Unescape(responseMessage).Trim('"');
            ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(response);

            responseMessage = this.apiCaller.Get(this.urls.ActionByProductId, new object[] { productDto.Id });
            response = Regex.Unescape(responseMessage).Trim('"');
            ActionDto actionDto = JsonConvert.DeserializeObject<ActionDto>(response);

            tfgetdiscount.Text = actionDto.Discount.ToString();
            tfgetthresholdamount.Text = actionDto.ThresholdAmount.ToString();
        }
    }
}
