using FreeCourse.FakePaymentService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.FakePaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePayment : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            return Ok("Payment process successfull.");
        }
    }
}
