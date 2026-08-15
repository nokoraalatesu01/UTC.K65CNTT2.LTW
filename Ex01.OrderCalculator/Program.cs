using Ex01.OrderCalculator.Enums;
using Ex01.OrderCalculator.Helpers;
using Ex01.OrderCalculator.Models;
using System.Globalization;
using System.Text;

Console.WriteLine("Hello, World!");
// Cấu hình hiển thị tiếng Việt và định dạng tiền tệ VNĐ
Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;
CultureInfo culture = new CultureInfo("vi-VN");

Console.WriteLine("==================================================");
Console.WriteLine("    CHƯƠNG TRÌNH TÍNH TIỀN ĐƠN HÀNG (DEVMASTER)   ");
Console.WriteLine("==================================================\n");

Order order = InputOrderInfo();
PrintInvoice(order, culture);

static Order InputOrderInfo()
{
    Order order = new Order();

    order.OrderId = InputHelper.ReadNonEmptyString("Nhập mã đơn hàng: ");
    order.CustomerName = InputHelper.ReadNonEmptyString("Nhập tên khách hàng: ");
    order.PhoneNumber = InputHelper.ReadPhoneNumber("Nhập số điện thoại (9-11 số): ");
    order.ProductName = InputHelper.ReadNonEmptyString("Nhập tên sản phẩm: ");
    order.Quantity = InputHelper.ReadInt("Nhập số lượng (>0): ", min: 1);
    order.UnitPrice = InputHelper.ReadDecimal("Nhập đơn giá (>=0): ", min: 0);
    order.DiscountPercentage = InputHelper.ReadDecimal("Nhập phần trăm giảm giá (0 - 50%): ", min: 0, max: 50);

    Console.WriteLine("\nChọn phương thức giao hàng:");
    Console.WriteLine("1. Giao hàng tiêu chuẩn (Standard - 20.000 VNĐ)");
    Console.WriteLine("2. Giao hàng nhanh (Express - 35.000 VNĐ)");
    Console.WriteLine("3. Giao hàng hỏa tốc (Fast - 50.000 VNĐ)");
    int shippingChoice = InputHelper.ReadInt("Lựa chọn (1-3): ", min: 1, max: 3);
    order.Shipping = (ShippingMethod)shippingChoice;

    return order;
}

static void PrintInvoice(Order order, CultureInfo culture)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("==================================================");
    Console.WriteLine("                 HÓA ĐƠN BÁN HÀNG                 ");
    Console.WriteLine("==================================================");
    Console.ResetColor();

    Console.WriteLine($"Mã đơn hàng   : {order.OrderId}");
    Console.WriteLine($"Khách hàng    : {order.CustomerName}");
    Console.WriteLine($"Số điện thoại : {order.PhoneNumber}");
    Console.WriteLine($"Sản phẩm      : {order.ProductName}");
    Console.WriteLine($"Số lượng      : {order.Quantity}");
    Console.WriteLine($"Đơn giá       : {order.UnitPrice.ToString("C0", culture)}");
    Console.WriteLine($"Giảm giá      : {order.DiscountPercentage}%");
    Console.WriteLine($"Phương thức GH: {order.Shipping}");
    Console.WriteLine("--------------------------------------------------");

    Console.WriteLine($"Thành tiền    : {order.SubTotal.ToString("C0", culture)}");
    Console.WriteLine($"Tiền giảm giá : -{order.DiscountAmount.ToString("C0", culture)}");

    if (order.SubTotal >= 2000000m)
    {
        Console.WriteLine("Phí vận chuyển: 0 VNĐ (Miễn phí vận chuyển cho đơn trên 2.000.000 VNĐ)");
    }
    else
    {
        Console.WriteLine($"Phí vận chuyển: {order.ShippingFee.ToString("C0", culture)}");
    }

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine($"TỔNG THANH TOÁN: {order.TotalPayment.ToString("C0", culture)}");
    Console.WriteLine("==================================================");
    Console.ResetColor();
}