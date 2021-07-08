using CommercialApplicationCommand.ApplicationLayer.Dtoes.Action;
using System.Collections.Generic;

namespace CommercialApplicationCommand.ApplicationLayer.Services.ActionServices
{
    public interface IActionAppService
    {
        IEnumerable<ActionDto> GetAll();
        ActionDto Get(long id);
        ActionDto GetByProductId(long productid);
        void CreateNewAction(ActionDto actionDto);
        void UpdateExistingAction(ActionDto actionDto);
        void RemoveExistingAction(ActionDto actionDto);
        void UpdateExistingActionByCustomerId(ActionDto actionDto);
        void UpdateExistingActionsBySalesChannel(ActionDto actionDto);
    }
}