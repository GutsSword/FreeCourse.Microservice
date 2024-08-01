using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.FakePaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePayment : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return Ok("Payment process successfull.");
        }
    }
}
