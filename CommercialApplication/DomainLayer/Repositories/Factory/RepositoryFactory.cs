using CommercialApplication.DomainLayer.Repositories.OrderRepositories.OrderItemRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.ActionRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.CustomerRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.InvoicesRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.OrderRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.ProductRepositories;
using CommercialApplicationCommand.DomainLayer.Repositories.StorageRepositories;

namespace CommercialApplicationCommand.DomainLayer.Repositories.Factory
{
    public static class RepositoryFactory
    {   public static ActionRepository CreateActionRepository()
        {
            return new ActionRepository();
        }

        public static CustomerRepository CreateCustomerRepository()
        {
            return new CustomerRepository();
        }

        public static InvoiceCustomerRepository CreateInvoiceCustomerRepository()
        {
            return new InvoiceCustomerRepository();
        }

        public static InvoiceItemInvoicesRepository CreateInvoiceItemInvoicesRepository()
        {
            return new InvoiceItemInvoicesRepository();
        }

        public static InvoiceItemRepository CreateInvoiceItemRepository()
        {
            return new InvoiceItemRepository();
        }

        public static InvoiceRepository CreateInvoiceRepository()
        {
            return new InvoiceRepository();
        }

        public static OrderCustomerRepository CreateOrderCustomerRepository()
        {
            return new OrderCustomerRepository();
        }

        public static OrderItemOrderRepository CreateOrderItemOrderRepository()
        {
            return new OrderItemOrderRepository();
        }

        public static OrderItemSelectRepository CreateOrderItemSelectRepository()
        {
            return new OrderItemSelectRepository();
        }

        public static OrderRepository CreateOrderRepository()
        {
            return new OrderRepository();
        }

        public static ProductRepository CreateProductRepository()
        {
            return new ProductRepository();
        }

        public static ProductStorageRepository CreateProductStorageRepository()
        {
            return new ProductStorageRepository();
        }

        public static StorageRepository CreateStorageRepository()
        {
            return new StorageRepository();
        }
    }
}