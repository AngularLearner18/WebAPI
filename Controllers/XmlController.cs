using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmlController : ControllerBase
    {
        [HttpPost("Xml")]
        public IActionResult Xml()
        {
            string response = string.Empty;
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var stringBuilder = new StringBuilder();

                    var element = XElement.Parse(reader.ReadToEnd());

                    var settings = new XmlWriterSettings();
                    settings.OmitXmlDeclaration = true;
                    settings.Indent = true;
                    settings.NewLineOnAttributes = true;

                    using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
                    {
                        element.Save(xmlWriter);
                    }

                    response = stringBuilder.ToString();
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }
    }
}