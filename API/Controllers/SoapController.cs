using DAL.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoapController : ControllerBase
    {
        private ISoapService soapService;

        public SoapController(ISoapService soapService)
        {
            this.soapService = soapService;
        }

        [HttpPost]
        public IActionResult Get([FromBody] XElement soapRequest)
        {
            var soapResponse = soapService.HandleSoapRequest(soapRequest);
            return Content(soapResponse.ToString(), "application/xml");
        }

        [HttpPost("xpath")]
        public IActionResult GetXPath([FromBody] XElement soapRequest, string XPath)
        {
            var soapResponse = soapService.HandleSoapRequest(soapRequest, XPath);
            return Content(soapResponse.ToString(), "application/xml");
        }
    }
}
