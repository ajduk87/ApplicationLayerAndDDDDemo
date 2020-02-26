﻿using CommercialClientApplication.DataGridModels;
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

namespace CommercialClientApplication
{
    /// <summary>
    /// Interaction logic for CustomerControl.xaml
    /// </summary>
    public partial class CustomerControl : UserControl
    {


        private readonly RegistrationServices registrationServices = new RegistrationServices();
        private readonly ICustomerService customerService;

        public ObservableCollection<Customer> Customers = new ObservableCollection<Customer>();
        public ICollectionView cvCustomers;

        private readonly CustomerUrls urls;

        private readonly IApiCaller apiCaller;

        public CustomerControl()
        {
            InitializeComponent();

            this.customerService = registrationServices.Container.Resolve<ICustomerService>();

            Customer customer = new Customer { Name = "Marko Ivic" };
            Customers.Add(customer);

            cvCustomers = CollectionViewSource.GetDefaultView(Customers);
            if (cvCustomers != null)
            {
                dgCustomerList.ItemsSource = cvCustomers;
            }
        }

        private void BtnEnterCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerDto customerDto = new CustomerDto
            {
                Name = tfentername.Text,
            };

            this.apiCaller.Post(this.urls.Customer, customerDto);
        }

        private void BtnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGetCustomerList_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
