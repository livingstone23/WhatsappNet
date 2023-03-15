using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WhatsapNet.Api.Model;
using WhatsapNet.Api.Service.WhatsappCloud.SendMessage;
using WhatsapNet.Api.Util;

namespace WhatsapNet.Api.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsappController : Controller
    {

        private readonly IWhatsappCloudSendMessage _whatsappCloudSendMessage;
        private readonly IUtil _util;

        public WhatsappController(IWhatsappCloudSendMessage whatsappCloudSendMessage, IUtil util)
        {
            _whatsappCloudSendMessage = whatsappCloudSendMessage;
            _util = util;
        }
        

        [HttpGet("test")]
        public async Task<IActionResult> Sample()
        {
            var data = new
            {
                messaging_product = "whatsapp",
                to = "34698971985",
                type = "text", 
                text = new
                {
                    body ="Probando interfaz de la api"
                }
            };

            var result = await _whatsappCloudSendMessage.Execute(data);
            return Ok("Ok sample");
        }


        [HttpGet]
        public IActionResult VerifyToken()
        {
            String AcessToken = "SAHKDSDTTAEFE232256456EWRE43";

            var token = Request.Query["hub.verify_token"].ToString();
            var challenge = Request.Query["hub.challenge"].ToString();

            if (challenge != null && token != null && token == AcessToken)
            {
                return Ok(challenge);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> ReceivedMessage([FromBody] WhatsAppCloudModel body )
        {
            try
            {

                var Message = body.Entry[0]?.Changes[0]?.Value?.Messages[0];
                if (Message != null)
                {
                    var userNumber = Message.From;
                    var userText = GetUserText(Message);
                }
                    return Ok("EVENT_RECEIVED");
            }
            catch (Exception e)
            {
                return Ok("EVENT_RECEIVED");
            }


        }

        private string GetUserText(Message message)
        {
            string TypeMessage = message.Type;

            if (TypeMessage.ToUpper() == "TEXT")
            {
                return message.Text.Body;
            }
            else if (TypeMessage.ToUpper() == "INTERACTIVE")
            {
                string interactiveType = message.Interactive.Type;

                if (interactiveType.ToUpper() == "LIST_REPLY")
                {
                    return message.Interactive.List_Reply.Title;
                }
                else if (interactiveType.ToUpper() == "BUTTON_REPLY")
                {
                    return message.Interactive.Button_Reply.Title;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
