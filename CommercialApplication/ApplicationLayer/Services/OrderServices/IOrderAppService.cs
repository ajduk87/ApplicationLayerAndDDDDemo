﻿using CommercialApplicationCommand.ApplicationLayer.Dtoes.Order;

namespace CommercialApplicationCommand.ApplicationLayer.Services.OrderServices
{
    public interface IOrderAppService
    {
        OrderDto GetOrder(long id);
        void CreateNewOrder(OrderDto orderDto);

        void UpdateExistingOrder(OrderDto orderDto);

        void DeleteExistingOrder(long id);
    }
}