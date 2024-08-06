using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService basketService;
        private readonly ICatologService catologService;

        public BasketController(IBasketService basketService, ICatologService catologService)
        {
            this.basketService = basketService;
            this.catologService = catologService;
        }

        public async Task<IActionResult>Index()
        {
            var response = await basketService.GetBasket();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> AddToBasket(string id)
        {
            var courseItem = await catologService.GetByCourseId(id);
            var basketItem = new BasketItemViewModel
            {
                CourseId = courseItem.CourseId,
                Price = courseItem.Price,
                CourseName = courseItem.Name,
            };

            await basketService.AddBasketItem(basketItem);

            return RedirectToAction("Index","Basket");
        }

        public async Task<IActionResult> DeleteBasketItem(string id)
        {       
            await basketService.RemoveBasketItem(id);
            return View();
        }
    }
}
