using CommercialClientApplication.DataGridModels;
using CommercialClientApplication.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using CommercialClientApplication.Dtoes;
using CommercialClientApplication.Urls;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CommercialClientApplication
{
    /// <summary>
    /// Interaction logic for StorageControl.xaml
    /// </summary>
    public partial class StorageControl : UserControl
    {

        private readonly RegistrationServices registrationServices = new RegistrationServices();
        private readonly IStorageService storageService;


        public ObservableCollection<StorehouseItem> StorehouseItems = new ObservableCollection<StorehouseItem>();
        public ICollectionView cvStorehouseItems;

        private readonly StorageUrls urls;

        private readonly IApiCaller apiCaller;


        public StorageControl()
        {
            InitializeComponent();

            this.storageService = registrationServices.Container.Resolve<IStorageService>();

            //StorehouseItem StorehouseItem = new StorehouseItem
            //{
            //    ProductName = "kafa",
            //    StorageName = "Doncafe Beograd",
            //    Amount = 15
            //};
            //StorehouseItem StorehouseItem2 = new StorehouseItem
            //{
            //    ProductName = "sok od jabuke",
            //    StorageName = "Nectar Beograd",
            //    Amount = 15
            //};

            //StorehouseItems.Add(StorehouseItem);
            //StorehouseItems.Add(StorehouseItem2);

            this.urls = new StorageUrls();

            this.apiCaller = registrationServices.Container.Resolve<IApiCaller>();

            cvStorehouseItems = CollectionViewSource.GetDefaultView(StorehouseItems);
            if (cvStorehouseItems != null)
            {
                dgStorageState.ItemsSource = cvStorehouseItems;
            }
        }

        private void BtnEnterStorage_Click(object sender, RoutedEventArgs e)
        {
            StorageDto storageDto = new StorageDto
            {
                Name = tfentername.Text,
                Location = tfenterlocation.Text
            };

            this.apiCaller.Post(this.urls.Storage, storageDto);
        }

        private void BtnUpdateStorage_Click(object sender, RoutedEventArgs e)
        {

            StorageDto storageDto = new StorageDto
            {
                Name = tfupdatename.Text,
                Location = tfupdatelocation.Text
            };

            string responseMessage = this.apiCaller.Get(this.urls.Storage, new object[] { storageDto.Name });
            string response = Regex.Unescape(responseMessage).Trim('"');
            storageDto.Id = JsonConvert.DeserializeObject<StorageDto>(response).Id;

            this.apiCaller.Put(this.urls.Storage, storageDto);
        }

        private void BtnGetStorageLocation_Click(object sender, RoutedEventArgs e)
        {
            string name = tfgetname.Text;

            string responseMessage = this.apiCaller.Get(this.urls.Storage, new object[] { name });
            string response = Regex.Unescape(responseMessage).Trim('"');
            StorageDto storageDto = JsonConvert.DeserializeObject<StorageDto>(response);

            tfgetlocation.Text = storageDto.Location;
        }

        private void BtnGetStorageState_Click(object sender, RoutedEventArgs e)
        {
            string name = tfstatename.Text;

            string responseMessage = this.apiCaller.Get(this.urls.StorageContent, new object[] { name });
            string response = Regex.Unescape(responseMessage).Trim('"');
            ObservableCollection<ProductStorageDto> productStorageDtoes = JsonConvert.DeserializeObject<ObservableCollection<ProductStorageDto>>(response);

            ObservableCollection<StorehouseItem> storehouseItems = new ObservableCollection<StorehouseItem>();
            foreach (ProductStorageDto productStorageDto in productStorageDtoes)
            {
                long storageid = productStorageDto.StorageId;

                responseMessage = this.apiCaller.Get(this.urls.StorageById, new object[] { storageid });
                response = Regex.Unescape(responseMessage).Trim('"');
                StorageDto storageDto = JsonConvert.DeserializeObject<StorageDto>(response);

                long productid = productStorageDto.ProductId;

                responseMessage = this.apiCaller.Get(this.urls.ProductById, new object[] { productid });
                response = Regex.Unescape(responseMessage).Trim('"');
                ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(response);

                StorehouseItem storehouseItem = new StorehouseItem
                {
                    StorageName = storageDto.Name,
                    ProductName = productDto.Name,
                    Amount = productStorageDto.AmountOfProduct
                };
                storehouseItems.Add(storehouseItem);
            }

            dgStorageState.ItemsSource = storehouseItems;
        }
    }
}
