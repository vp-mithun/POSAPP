using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PosAPI.Controllers
{
    [Route("posapi/api/[controller]")]
    public class ValuesController : Controller
    {
        public ValuesController()
        {
            source.Add("arabic", "Ø¨ÙØ³Ù’Ù…Ù Ø§Ù„Ù„Ù‡Ù Ø§Ù„Ø±ÙŽÙ‘Ø­Ù’Ù…Ù°Ù†Ù Ø§Ù„Ø±ÙŽÙ‘Ø­ÙÙŠÙ’Ù…Ù");
            source.Add("urdu", "ÛŒÙˆÙ†ÛŒÚ©ÙˆÚˆ ÚˆÛŒÙ¹Ø§ Ù…ÛŒÚº Ø§Ø±Ø¯Ùˆ");
            source.Add("hindi", "à¤¯à¥‚à¤¨à¤¿à¤•à¥‹à¤¡ à¤¡à¥‡à¤Ÿà¤¾ à¤®à¥‡à¤‚ à¤¹à¤¿à¤‚à¤¦à¥€");
            source.Add("russian", "Ñ€Ñ†Ñ‹ ÑÐ»Ð¾Ð²Ð¾ Ñ‚Ð²ÐµÑ€Ð´Ð¾");
            source.Add("english", "Love for all, hatred for none!");
        }
        private Dictionary<string, string> source = new Dictionary<string, string>();

        [HttpGetAttribute]
        public Dictionary<string, string> GetData()
        {
            return source;
        }

        [HttpGetAttribute]        
        [RouteAttribute("{language}")]
        //public string GetItem(string language)
        //{
        //    switch (language.ToLower())
        //    {
        //        case "arabic":
        //            return source["arabic"];
        //        case "urdu":
        //            return source["urdu"];
        //        case "hindi":
        //            return source["hindi"];
        //        case "russian":
        //            return source["russian"];
        //        case "english":
        //            return source["english"];
        //        default:
        //            return "Language not found.";
        //    }
        //}

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "ગુજરાતી કીબોર્ડ", "ಕನ್ನಡ " };
        }

        
        [HttpGet("{id}")]
        [Produces("application/json;")]
        public IActionResult Get(int id)
        {
            //return "ગુજરાતી કીબોર્ડ";
            var uu = "{ 'ગુજરાતી કીબોર્ડ', 'ಕನ್ನಡ' }";
            return Ok( uu);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
