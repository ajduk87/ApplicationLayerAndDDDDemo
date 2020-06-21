using Autofac;
using CommercialApplication.ApplicationLayer.Models.Invoices;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Invoices;
using CommercialApplicationCommand.ApplicationLayer.Services.InvoicesService;
using CommercialApplicationCommand.ApplicationLayer.Validation;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommercialApplicationCommand.ApplicationLayer.Controllers
{
    public class InvoicesController : BaseController
    {
        private readonly IInvoicesAppService invoicesAppService;

        public InvoicesController(IInvoicesAppService invoicesAppService)
        {
            this.invoicesAppService = invoicesAppService;
            //this.invoicesAppService = this.registrationAppServices.Instance.Container.Resolve<IInvoicesAppService>();
        }

        [HttpGet]
        [Route("api/invoice")]
        public InvoiceViewModel GetInvoice(long id)
        {
            InvoiceDto invoiceDto = this.invoicesAppService.GetInvoice(id);
            InvoiceViewModel invoiceViewModel = this.mapper.Map<InvoiceViewModel>(invoiceDto);

            return invoiceViewModel;
        }

        [HttpPost]
        [Route("api/invoice")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Insert(InvoiceCreateModel invoiceCreateModel)
        {
            InvoiceDto invoicesDto = this.mapper.Map<InvoiceCreateModel, InvoiceDto>(invoiceCreateModel);
            this.invoicesAppService.CreateNewInvoice(invoicesDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/invoice")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Delete(InvoiceDeleteModel invoicesDeleteModel)
        {
            InvoiceDto invoicesDto = this.mapper.Map<InvoiceDeleteModel, InvoiceDto>(invoicesDeleteModel);
            this.invoicesAppService.RemoveExistingInvoice(invoicesDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/maxsumvalueinvoice")]
        public InvoiceViewModel GetMaxSumValueInvoice(DateTime day)
        {
            InvoiceDto invoiceDto = this.invoicesAppService.GetMaxSumValueInvoiceForDay(day);
            InvoiceViewModel invoiceViewModel = this.mapper.Map<InvoiceViewModel>(invoiceDto);

            return invoiceViewModel;
        }

        [HttpGet]
        [Route("api/minsumvalueinvoice")]
        public InvoiceViewModel GetMinSumValueInvoice(DateTime day)
        {
            InvoiceDto invoiceDto = this.invoicesAppService.GetMinSumValueInvoiceForDay(day);
            InvoiceViewModel invoiceViewModel = this.mapper.Map<InvoiceViewModel>(invoiceDto);

            return invoiceViewModel;
        }
    }
}