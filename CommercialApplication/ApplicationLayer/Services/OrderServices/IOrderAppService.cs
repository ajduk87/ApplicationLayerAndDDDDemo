using CommercialApplication.ApplicationLayer.Dtoes.Order;
using CommercialApplicationCommand.ApplicationLayer.Dtoes.Order;
using System;

namespace CommercialApplicationCommand.ApplicationLayer.Services.OrderServices
{
    public interface IOrderAppService
    {
        OrderDto GetOrder(long id);
        OrderDto GetMaxSumValueOrderForDay(DateTime day);
        OrderDto GetMinSumValueOrderForDay(DateTime day);
        void CreateNewOrder(OrderDto orderDto);

        void ModiifyExistingOrder(OrderDto orderDto);

        void RemoveExistingOrder(long id);
        void SetState(OrderStateDto orderStateDto);
    }
}