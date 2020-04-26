using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LiqPay.Models;
using LiqPay.Service;
using Newtonsoft.Json;

namespace LiqPay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(LiqpayService.GetLiqPayModel(Guid.NewGuid().ToString()));
        }

        /// <summary>
        /// On this page Liqpay send payment result. 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RedirectToPayment()
        {
            // Convert Liqpay response to Dictionary<string, string>
            var responseDictionary = Request.Form.Keys.ToDictionary(key => Request.Form[key]);

            // Decode parameter Liqpay response data and converting to dictionary 
            var responseData = Convert.FromBase64String(responseDictionary["data"]);
            var decodedString = Encoding.UTF8.GetString(responseData);
            var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(decodedString);

            // Get signature for check
            var mySignature = LiqpayService.GetSignature(responseDictionary["data"]);

            // Check is equal signatures
            if (mySignature != responseDictionary["signature"])
            {
                return View("Error");
            }

            if (jsonResponse["status"] == "success" || jsonResponse["status"] == "sandbox")
            {
                // Here you can update order status. Order Id you can find here: jsonResponse["order_id"]

                return View("_Success");
            }
            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
