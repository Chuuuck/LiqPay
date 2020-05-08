# LiqPay API example for .NET
<a href="https://www.liqpay.ua/"> Liqpay </a> is payment system associated with <a href="https://privatbank.ua/"> Privat Bank </a>.

API documentation <a href="https://www.liqpay.ua/documentation/en"> in English </a> and <a href="https://www.liqpay.ua/documentation/ru"> in Russian </a>.

## Usage: 
Use it for send all checkout parameters to Liqpay API:

```
 // Fill data for submit them to the view
            var signatureSource = new LiqpayCheckoutViewModel
            {
                PublicKey = _publicKey,
                Version = 3,
                Action = "pay",
                Amount = 1,
                Currency = "UAH",
                Description = "Test order payment",
                OrderId = orderId,
                Sandbox = 1,

                ResultUrl = "https://localhost:44311/Home/Redirect",

                ProductCategory = "Food",
                ProductName = "Pizza BBQ",
                ProductDescription = "Hot and delicious bbq pizza"
            };
