using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WhatsapNet.Api.Model;
using WhatsapNet.Api.Service.WhatsappCloud.SendMessage;
using WhatsapNet.Api.Util;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            string AcessToken = "SAHKDSDTTAEFE232256456EWRE43";

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



        /// <summary>
        /// Metodo para recibir el mensaje enviado por el cliente.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
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

                    object objectMessage;

                    if (userText.ToUpper().Contains("BUENAS"))
                    {
                        objectMessage = _util.TextMessage("Hola, Bienvenido a PROVIMAD, ¿Como te puedo ayudar?. ", userNumber);
                    }
                    else if (userText.ToUpper().Contains("AYUDA"))
                    {
                        objectMessage = _util.TextMessage("Hola, estas en el servicio de WhatsApp PROVIMAD, ¡Para ir al menu principal, escribir la palabra 'MENU'! ", userNumber);
                    }
                    else
                    {

                        switch (userText.ToUpper())
                        {

                            //case "TEXT":
                            //    objectMessage = _util.TextMessage("Este es un ejemplo de texto", userNumber);
                            //    break;

                            //case "IMAGE":
                            //    objectMessage = _util.ImageMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/image_whatsapp.png", userNumber);
                            //    break;

                            //case "AUDIO":
                            //    objectMessage = _util.AudioMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/audio_whatsapp.mp3", userNumber);
                            //    break;
                                
                            //case "VIDEO":
                            //    objectMessage = _util.VideoMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/video_whatsapp.mp4", userNumber);
                            //    break;

                            case "DOCUMENT":
                                objectMessage = _util.DocumentMessage("https://biostoragecloud.blob.core.windows.net/resource-udemy-whatsapp-node/document_whatsapp.pdf", userNumber);
                                break;

                            ///revisar location
                            case "LOCATION":
                            case "LOCALIZACIÓN":
                                objectMessage = _util.LocationMessage(userNumber);
                                break;

                            case "MENU":
                            case "HOLA":
                                objectMessage = _util.ButtonMessage(userNumber);
                                break;

                            default:
                                objectMessage = _util.TextMessage("Este es un ejemplo de texto", userNumber);
                                break;
                        }
                    

                    
                    }
                    await _whatsappCloudSendMessage.Execute(objectMessage);
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
