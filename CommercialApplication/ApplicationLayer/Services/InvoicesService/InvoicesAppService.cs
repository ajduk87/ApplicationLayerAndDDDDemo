using System;
using System.Collections.Generic;
using Autofac;
using CommercialApplication.DomainLayer.Entities.ValueObjects.Common;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Invoices;
using CommercialApplicationCommand.DomainLayer.Entities.CustomerEntities;
using CommercialApplicationCommand.DomainLayer.Entities.InvoicesEntities;
using CommercialApplicationCommand.DomainLayer.Entities.OrderEntities;
using CommercialApplicationCommand.DomainLayer.Services.InvoicesServices;
using CommercialApplicationCommand.DomainLayer.Services.OrderServices;
using Npgsql;

namespace CommercialApplicationCommand.ApplicationLayer.Services.InvoicesService
{
    public class InvoicesAppService : BaseAppService, IInvoicesAppService
    {
        private readonly IInvoiceCustomerService invoiceCustomerService;
        private readonly IInvoiceItemInvoiceService invoiceItemInvoicesService;
        private readonly IInvoiceItemService invoiceItemService;
        private readonly IInvoiceService invoicesService;
        private readonly IOrderService orderService;

        public InvoicesAppService(IInvoiceCustomerService invoiceCustomerService,
                                  IInvoiceItemInvoiceService invoiceItemInvoicesService,
                                  IInvoiceItemService invoiceItemService,
                                  IInvoiceService invoicesService,
                                  IOrderService orderService)
        {
            this.invoiceCustomerService = invoiceCustomerService;
            this.invoiceItemInvoicesService = invoiceItemInvoicesService;
            this.invoiceItemService = invoiceItemService;
            this.invoicesService = invoicesService;
            this.orderService = orderService;
            //this.invoicesService = this.registrationServices.Instance.Container.Resolve<IInvoiceService>();
            //this.invoiceItemService = this.registrationServices.Instance.Container.Resolve<IInvoiceItemService>();
            //this.invoiceItemInvoicesService = this.registrationServices.Instance.Container.Resolve<IInvoiceItemInvoiceService>();
            //this.invoiceCustomerService = this.registrationServices.Instance.Container.Resolve<IInvoiceCustomerService>();
            //this.orderService = this.registrationServices.Instance.Container.Resolve<IOrderService>();
        }

        private InvoiceDto GetLookForInvoice(long id)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                Invoice invoice = this.invoicesService.SelectById(connection, id);
                IEnumerable<long> invoiceItemsIds = this.invoiceItemInvoicesService.SelectByInvoiceId(connection, invoice.Id);
                IEnumerable<InvoiceItem> invoiceItems = this.invoiceItemService.SelectByIds(connection, invoiceItemsIds);
                IEnumerable<InvoiceItemDto> invoiceItemDtoes = this.dtoToEntityMapper.MapViewList<IEnumerable<InvoiceItem>, IEnumerable<InvoiceItemDto>>(invoiceItems);
                Customer customer = this.invoiceCustomerService.SelectByInvoiceId(connection, invoice.Id);

                Order order = this.orderService.SelectById(connection, invoice.OrderId);

                return new InvoiceDto
                {
                    CustomerId = customer.Id,
                    SellingProgramId = invoice.SellingProgramId,
                    OrderId = invoice.OrderId,
                    InvoiceItems = invoiceItemDtoes
                };
            }
        }

        public InvoiceDto GetInvoice(long id)
        {
            return this.GetLookForInvoice(id);
        }

        public void RemoveExistingInvoice(InvoiceDto invoiceDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                connection.Open();
                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        IEnumerable<long> invoiceItemIds = this.invoiceItemInvoicesService.SelectByInvoiceId(connection, invoiceDto.Id);
                        this.invoiceItemInvoicesService.Delete(connection, invoiceDto.Id, transaction);
                        this.invoiceCustomerService.Delete(connection, invoiceDto.Id, transaction);
                        this.invoiceItemService.DeleteByIds(connection, invoiceItemIds, transaction);
                        this.invoicesService.Delete(connection, invoiceDto.Id, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write(ex);
                    }
                }
            }
        }

        public void CreateNewInvoice(InvoiceDto invoiceDto)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                connection.Open();
                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        Invoice invoice = this.dtoToEntityMapper.Map<InvoiceDto, Invoice>(invoiceDto);
                        long invoiceId = this.invoicesService.Insert(connection, invoice);
                        InvoiceCustomerDto invoiceCustomerDto = new InvoiceCustomerDto
                        {
                            InvoiceId = invoiceId,
                            CustomerId = invoiceDto.CustomerId
                        };
                        InvoiceCustomer invoiceCustomer = this.dtoToEntityMapper.Map<InvoiceCustomerDto, InvoiceCustomer>(invoiceCustomerDto);
                        this.invoiceCustomerService.Insert(connection, invoiceCustomer);
                        IEnumerable<InvoiceItem> invoiceItems = this.dtoToEntityMapper.MapList<IEnumerable<InvoiceItemDto>, IEnumerable<InvoiceItem>>(invoiceDto.InvoiceItems);
                        IEnumerable<InvoiceItem> calculatedInvoiceItemsWithBasicDiscount = this.invoiceItemService.IncludeBasicDiscountForPaying(connection, invoiceItems);
                        IEnumerable<InvoiceItem> calculatedInvoiceItemsWithBasicAndActionDiscount = this.invoiceItemService.IncludeActionDiscountForPaying(connection, invoiceItems);
                        this.invoiceItemService.InsertList(connection, calculatedInvoiceItemsWithBasicAndActionDiscount, transaction);
                        this.invoiceItemInvoicesService.InsertList(connection, calculatedInvoiceItemsWithBasicDiscount, invoiceId, transaction);

                        Order order = this.orderService.SelectById(connection, invoice.OrderId);
                        order.State = new State("Close");
                        this.orderService.Update(connection, order);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.Write(ex.Message);
                    }
                }
            }
        }

        public InvoiceDto GetMaxSumValueInvoiceForDay(DateTime day)
        {
            using (NpgsqlConnection connection = this.databaseConnectionFactory.Instance.Create())
            {
                IEnumerable<Invoice> invoices = this.invoicesService.SelectByDay(connection, day.ToShortDateString());

                long orderIdWithMaxSumValue = this.invoicesService.SelectInvoiceIdWithMaxSumValueByDay(connection, invoices);

                return this.GetLookForInvoice(orderIdWithMaxSumValue);
            }
        }

        public InvoiceDto GetMinSumValueInvoiceForDay(DateTime day)
        {
            return new InvoiceDto();
        }
    }
}