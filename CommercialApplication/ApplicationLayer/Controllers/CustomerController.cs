using Autofac;
using CommercialApplication.ApplicationLayer.Models.Customer;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Customer;
using CommercialApplicationCommand.ApplicationLayer.Models.Customer;
using CommercialApplicationCommand.ApplicationLayer.Services.CustomerServices;
using CommercialApplicationCommand.ApplicationLayer.Validation;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommercialApplicationCommand.ApplicationLayer.Controllers
{
    public class CustomerController : BaseController
    {
        private ICustomerAppService customerAppService;


        private CustomerController(ICustomerAppService customerAppService)
        {
            this.customerAppService = customerAppService;
            //this.customerAppService = this.registrationAppServices.Instance.Container.Resolve<ICustomerAppService>();
        }

        public CustomerController(CustomerAppService customerAppService)
        {
            this.customerAppService = customerAppService;
        }

        [HttpGet]
        [Route("api/customer")]
        public IEnumerable<CustomerViewModel> GetAll()
        {
            IEnumerable<CustomerDto> customerDtoes = customerAppService.GetAll();
            IEnumerable<CustomerViewModel> customerViewModels = this.mapper.Map<IEnumerable<CustomerViewModel>>(customerDtoes);
            return customerViewModels;
        }

        [HttpGet]
        [Route("api/customer/{id}")]
        public CustomerViewModel Get(long id)
        {
            CustomerDto customerDto = customerAppService.Get(id);
            CustomerViewModel customerViewModel = this.mapper.Map<CustomerViewModel>(customerDto);
            return customerViewModel;
        }

        [HttpGet]
        [Route("api/customerbyname/{name}")]
        public CustomerViewModel Get(string name)
        {
            CustomerDto customerDto = customerAppService.GetByName(name);
            CustomerViewModel customerViewModel = this.mapper.Map<CustomerViewModel>(customerDto);
            return customerViewModel;
        }

        [HttpPost]
        [Route("api/customer")]
        [ValidateModelStateFilter]
        public HttpResponseMessage CreateNewCustomerInfo(CustomerCreateModel customerCreateModel)
        {
            CustomerDto customerDto = this.mapper.Map<CustomerCreateModel, CustomerDto>(customerCreateModel);
            this.customerAppService.CreateNewCustomerInfo(customerDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/customer")]
        [ValidateModelStateFilter]
        public HttpResponseMessage UpdateExistingCustomerInfo(CustomerUpdateModel customerUpdateModel)
        {
            CustomerDto customerDto = this.mapper.Map<CustomerUpdateModel, CustomerDto>(customerUpdateModel);
            this.customerAppService.UpdateExistingCustomerInfo(customerDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/customer")]
        [ValidateModelStateFilter]
        public HttpResponseMessage RemoveExistingCustomerInfo(CustomerDeleteModel customerDeleteModel)
        {
            CustomerDto customerDto = this.mapper.Map<CustomerDeleteModel, CustomerDto>(customerDeleteModel);
            this.customerAppService.RemoveExistingCustomerInfo(customerDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}