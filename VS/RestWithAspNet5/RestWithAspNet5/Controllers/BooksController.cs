using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithAspNet5.Model;
using RestWithAspNet5.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class BooksController : ControllerBase
    {
       

        private readonly ILogger<BooksController> _logger;
        private Business.IBooksBusiness _bookBusiness;

        public BooksController(ILogger<BooksController> logger, Business.IBooksBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());

        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var books = _bookBusiness.FindById(id);

            if (books == null) return NotFound();
            return Ok(books);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Books book)
        {
          if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));

        }

        [HttpPut]
        public IActionResult Put([FromBody] Books book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));

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
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
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
