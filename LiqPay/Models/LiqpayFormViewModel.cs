namespace LiqPay.Models
{
    /// <summary>
    /// Data that is passed to the form view for generate a payment button Liqpay  
    /// </summary>
    public class LiqpayFormViewModel
    {
        public string Data { get; set; }
        public string Signature { get; set; }
    }
}
