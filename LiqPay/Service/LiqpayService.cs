using System;
using System.Security.Cryptography;
using System.Text;
using LiqPay.Models;
using Newtonsoft.Json;

namespace LiqPay.Service
{
    public class LiqpayService
    {
        private static readonly string _privateKey;
        private static readonly string _publicKey;

        static LiqpayService()
        {
            // Private and public keys that can find in the personal cabinet on the site liqpay.ua
            _privateKey = "******";
            _publicKey = "******";
        }

        /// <summary>
        /// Generate data for Liqpay (data, signature)
        /// </summary>
        /// <param name="orderId">Order number</param>
        /// <returns></returns>
        public static LiqpayFormViewModel GetLiqPayModel(string orderId)
        {
            // Fill data for submit them to the view
            var signatureSource = new LiqpayCheckoutViewModel
            {
                PublicKey = _publicKey,
                Version = 3,
                Action = "pay",
                Amount = 5,
                Currency = "UAH",
                Description = "Test order payment",
                OrderId = orderId,
                Sandbox = 1,

                ResultUrl = "https://localhost:1274/Home/Redirect",

                ProductCategory = "Food",
                ProductName = "Pizza BBQ",
                ProductDescription = "Hot and delicious bbq pizza"
            };
            var jsonString = JsonConvert.SerializeObject(signatureSource);
            var dataHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
            var signatureHash = GetSignature(dataHash);

            // Data for send to view
            var model = new LiqpayFormViewModel
            {
                Data = dataHash, 
                Signature = signatureHash
            };
            return model;
        }

        /// <summary>
        /// Generate liqpay signature
        /// </summary>
        /// <param name="data">Json string with parameters for Liqpay</param>
        /// <returns></returns>
        public static string GetSignature(string data)
        {
            return Convert.ToBase64String(SHA1.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(_privateKey + data + _privateKey)));
        }
    }
}
