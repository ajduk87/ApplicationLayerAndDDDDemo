using Autofac;
using CommercialApplication.ApplicationLayer.Dtoes.Order;
using CommercialApplication.ApplicationLayer.Models.Order;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Order;
using CommercialApplicationCommand.ApplicationLayer.Services.OrderServices;
using CommercialApplicationCommand.ApplicationLayer.Validation;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommercialApplicationCommand.ApplicationLayer.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderAppService orderAppService;

        public OrderController()
        {
            this.orderAppService = this.registrationAppServices.Container.Resolve<IOrderAppService>();
        }

        [HttpGet]
        [Route("api/order")]
        public OrderViewModel GetOrder(long id)
        {
            OrderDto orderDto = this.orderAppService.GetOrder(id);
            OrderViewModel orderViewModel = this.mapper.Map<OrderViewModel>(orderDto);

            return orderViewModel;
        }

        [HttpGet]
        [Route("api/maxsumvalueorder")]
        public OrderViewModel GetMaxSumValueOrder(DateTime day)
        {
            OrderDto orderDto = this.orderAppService.GetMaxSumValueOrderForDay(day);
            OrderViewModel orderViewModel = this.mapper.Map<OrderViewModel>(orderDto);

            return orderViewModel;
        }

        [HttpGet]
        [Route("api/minsumvalueorder")]
        public OrderViewModel GetMinSumValueOrder(DateTime day)
        {
            OrderDto orderDto = this.orderAppService.GetMinSumValueOrderForDay(day);
            OrderViewModel orderViewModel = this.mapper.Map<OrderViewModel>(orderDto);

            return orderViewModel;
        }

        [HttpPost]
        [Route("api/order")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Insert(OrderCreateModel orderCreateModel)
        {
            OrderDto orderDto = this.mapper.Map<OrderDto>(orderCreateModel);
            this.orderAppService.CreateNewOrder(orderDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/order")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Update(OrderUpdateModel orderUpdateModel)
        {
            OrderDto orderDto = this.mapper.Map<OrderDto>(orderUpdateModel);
            this.orderAppService.ModifyExistingOrder(orderDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/setorderstate")]
        [ValidateModelStateFilter]
        public HttpResponseMessage SetState(OrderStateModel orderStateModel)
        {
            OrderStateDto orderStateDto = this.mapper.Map<OrderStateModel, OrderStateDto>(orderStateModel);
            this.orderAppService.SetState(orderStateDto);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/order")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Delete(OrderDeleteModel orderDeleteModel)
        {
            this.orderAppService.RemoveExistingOrder(orderDeleteModel.Id);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}