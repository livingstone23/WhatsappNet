using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace WhatsapNet.Api.Service.WhatsappCloud.SendMessage
{
    public class WhatsappCloudSendMessage: IWhatsappCloudSendMessage
    {
        public async Task<bool> Execute(object model)
        {
            var client = new HttpClient();

            var byteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            using (var content = new ByteArrayContent(byteData))
            {
                string endpoint = "https://graph.facebook.com";
                string phoneNumberId = "114893344880968";
                string accessToken = "EABSWDD8kElcBAHNkZBidoRYK6Fyp0VtsAEiv9dZAqi0tZA0wMwRfO9k7mCaYYGmyxHRsv2qxNZCkssspNjL0xB4dYKzdaZCoA9eEVbyhStAxf37V3rMaZClLRHOLNokoMTNzTi4u9ZBXxcWrwuIhLebmJRBdSQ0IQEnPUc2klaZAyrwFi3IVfDwq";
                string uri = $"{endpoint}/v15.0/{phoneNumberId}/messages";

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;

            }

        }
    }
}
