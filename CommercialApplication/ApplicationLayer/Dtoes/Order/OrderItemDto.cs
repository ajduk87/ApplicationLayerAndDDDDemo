﻿namespace CommercialApplicationCommand.ApplicationLayer.Dtoes.Order
{
    public class OrderItemDto : Dto
    {
        public long ProductId { get; set; }
        public int Amount { get; set; }
        public double DiscountBasic { get; set; }
        public long ActionId { get; set; }
    }
}