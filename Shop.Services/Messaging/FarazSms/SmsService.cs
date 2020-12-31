using Newtonsoft.Json;
using Shop.Services.Messaging.FarazSms.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services.Messaging.FarazSms
{
    public class SmsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpRequestMessage _request;
        private HttpClient client;
        public SmsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private void Init()
        {
            client = _httpClientFactory.CreateClient();

            _request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://ippanel.com/api/select"),
                Method = HttpMethod.Post
            };
        }
        public async Task SendSmsForConsomerOrder(Models.ConsumerOrderModel model)
        {
            Init();

            _request.Content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.SendAsync(_request);

            var responseStr = await response.Content.ReadAsStringAsync();
        }

        public async Task SendSmsForAdminOrder(Models.AdminOrderModel model)
        {
            Init();

            _request.Content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.SendAsync(_request);

            var responseStr = await response.Content.ReadAsStringAsync();
        }

        public async Task SendSmsForForgotPassword(ForgotPasswordModel model)
        {
            Init();

            _request.Content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.SendAsync(_request);

            var responseStr = await response.Content.ReadAsStringAsync();
        }
    }
}
