using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithAspNet5.Model;
using RestWithAspNet5.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Hypermedia.Filters;

namespace RestWithAspNet5.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class PersonController : ControllerBase
    {
       

        private readonly ILogger<PersonController> _logger;
        private Business.IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, Business.IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200),Type= typeof(List<PersonVO>)) ]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());

        }

        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person =_personBusiness.FindById(id);

            if (person == null) return NotFound();
            return Ok(person);

        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
         [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
          if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));

        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));

        }

        private decimal ConvertTodecimal(string strNumber)
        {
            decimal decimalValue;
            if (decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }

            return 0;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();


        }

        private bool isNumeric(string strNumber)
        {
            double number;
            bool isnumber = double.TryParse(strNumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out number);
            return isnumber;

        }
    }
}
