using Autofac;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Action;
using CommercialApplicationCommand.ApplicationLayer.Models.Action;
using CommercialApplicationCommand.ApplicationLayer.Services.ActionServices;
using CommercialApplicationCommand.ApplicationLayer.Validation;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CommercialApplicationCommand.ApplicationLayer.Controllers
{
    public class ActionController : BaseController
    {
        private readonly IActionAppService actionAppService;

        public ActionController(IActionAppService actionAppService)
        {
            this.actionAppService = actionAppService;
            //this.actionAppService = this.registrationAppServices.Instance.Container.Resolve<IActionAppService>();
        }

        [HttpGet]
        [Route("api/action")]
        public IEnumerable<ActionViewModel> GetAll()
        {
            IEnumerable<ActionDto> actionDtoes = actionAppService.GetAll();
            IEnumerable<ActionViewModel> actionViewModels = this.mapper.Map<IEnumerable<ActionViewModel>>(actionDtoes);
            return actionViewModels;
        }

        [HttpGet]
        [Route("api/actionbyid/{id}")]
        public ActionViewModel Get(long id)
        {
            ActionDto actionDto = actionAppService.Get(id);
            ActionViewModel actionViewModel = this.mapper.Map<ActionViewModel>(actionDto);
            return actionViewModel;
        }

        [HttpGet]
        [Route("api/actionbyproductid/{productid}")]
        public ActionViewModel GetByProductId(long productid)
        {
            ActionDto actionDto = actionAppService.GetByProductId(productid);
            ActionViewModel actionViewModel = this.mapper.Map<ActionViewModel>(actionDto);
            return actionViewModel;
        }

        [HttpPost]
        [Route("api/action")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Insert(ActionCreateModel actionCreateModel)
        {
            ActionDto actionDto = this.mapper.Map<ActionCreateModel, ActionDto>(actionCreateModel);
            this.actionAppService.CreateNewAction(actionDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/action")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Update(ActionUpdateModel actionUpdateModel)
        {
            ActionDto actionDto = this.mapper.Map<ActionUpdateModel, ActionDto>(actionUpdateModel);
            this.actionAppService.UpdateExistingAction(actionDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("api/action/customer")]
        [ValidateModelStateFilter]
        public HttpResponseMessage UpdateActionByCustomerId(ActionCustomerUpdateModel actionCustomerUpdateModel)
        {
            ActionDto actionDto = this.mapper.Map<ActionCustomerUpdateModel, ActionDto>(actionCustomerUpdateModel);
            this.actionAppService.UpdateExistingActionByCustomerId(actionDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("api/action")]
        [ValidateModelStateFilter]
        public HttpResponseMessage Delete(ActionDeleteModel actionDeleteModel)
        {
            ActionDto actionDto = this.mapper.Map<ActionDeleteModel, ActionDto>(actionDeleteModel);
            this.actionAppService.RemoveExistingAction(actionDto);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}