using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using WhatsapNet.Api.Model;

namespace WhatsapNet.Api.Controllers
{
    [ApiController]
    [Route("api/whatsapp")]
    public class WhatsappController : Controller
    {
        [HttpGet("test")]
        public IActionResult Sample()
        {
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
                return Ok("EVENT_RECEIVED");
            }
            catch (Exception e)
            {
                return Ok("EVENT_RECEIVED");
            }


        }
    }
}
