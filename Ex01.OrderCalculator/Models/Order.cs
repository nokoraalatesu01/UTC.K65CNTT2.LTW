using Ex01.OrderCalculator.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex01.OrderCalculator.Models
{
    public class Order
    {
        public string OrderId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercentage { get; set; }
        public ShippingMethod Shipping { get; set; }

        // Các thuộc tính tính toán (Calculated Properties)
        public decimal SubTotal => Quantity * UnitPrice;

        public decimal DiscountAmount => SubTotal * (DiscountPercentage / 100m);
        public decimal ShippingFee
        {
            get
            {
                // Miễn phí vận chuyển nếu đơn hàng từ 2.000.000 VNĐ
                if (SubTotal >= 2000000m) return 0m;

                return Shipping switch
                {
                    ShippingMethod.Standard => 20000m,
                    ShippingMethod.Express => 35000m,
                    ShippingMethod.Fast => 50000m,
                    _ => 20000m
                };
            }
        }

        public decimal TotalPayment => SubTotal - DiscountAmount + ShippingFee;
    }
}
