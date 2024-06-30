using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Commons.Xml.Relaxng;
using DAL.Validator;
using DAL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private XMLValidator validator;
        private IWordRepository wordRepository;

        public WordController(IWordRepository wordRepository)
        {
            this.validator = new XMLValidator();
            this.wordRepository = wordRepository;
        }


        // GET: api/<WordController>
        [HttpGet]
        public IEnumerable<Word> Get()
        {
            return wordRepository.GetWords();
        }

        // POST api/<WordController>
        [HttpPost]
        public IActionResult Create([FromBody] XElement xml, string schemaType = "RNG")
        {
            var type = validator.GetSchemaType(schemaType);

            if (type == null)
            {
                return BadRequest();
            }

            if (!validator.Validate(xml, type.Value))
            {
                return BadRequest();
            }
            
            IList<Word> words = Word.GetWords(xml);
            wordRepository.AddWords(words);

            return Ok();
        }
    }
}
