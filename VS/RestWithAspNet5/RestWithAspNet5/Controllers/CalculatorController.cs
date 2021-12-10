using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
       

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber , string secondNumber)
        {

            if (isNumeric(firstNumber) && isNumeric(secondNumber))
            {
                var sum = ConvertTodecimal(firstNumber) + ConvertTodecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid input"); 

        }

        [HttpGet("substration/{firstNumber}/{secondNumber}")]
        public IActionResult Substration(string firstNumber, string secondNumber)
        {

            if (isNumeric(firstNumber) && isNumeric(secondNumber))
            {
                var sum = ConvertTodecimal(firstNumber) - ConvertTodecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid input");

        }

        [HttpGet("multiplication/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(string firstNumber, string secondNumber)
        {

            if (isNumeric(firstNumber) && isNumeric(secondNumber))
            {
                var sum = ConvertTodecimal(firstNumber) * ConvertTodecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid input");

        }

        [HttpGet("division/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {

            if (isNumeric(firstNumber) && isNumeric(secondNumber))
            {
                var sum = ConvertTodecimal(firstNumber) / ConvertTodecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid input");

        }

        [HttpGet("mean/{firstNumber}/{secondNumber}")]
        public IActionResult Mean(string firstNumber, string secondNumber)
        {

            if (isNumeric(firstNumber) && isNumeric(secondNumber))
            {
                var sum = ConvertTodecimal(firstNumber) + ConvertTodecimal(secondNumber)/2;
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid input");

        }

        [HttpGet("square-root/{firstNumber}")]
        public IActionResult SquareRoot(string firstNumber)
        {

            if (isNumeric(firstNumber) )
            {
                var squareroot = Math.Sqrt((double)ConvertTodecimal(firstNumber));
                return Ok(squareroot.ToString());
            }
            return BadRequest("Invalid input");

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

        private bool isNumeric(string strNumber)
        {
            double number;
            bool isnumber = double.TryParse(strNumber, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out number);
            return isnumber;

        }
    }
}
