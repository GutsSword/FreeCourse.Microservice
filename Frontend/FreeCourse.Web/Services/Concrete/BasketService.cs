using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;
using System.Net.Http;

namespace FreeCourse.Web.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient httpClient;

        public BasketService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            var basket = await GetBasket();

            if (basket != null)
            {
                if (!basket.BasketItem.Any(x => x.CourseId == basketItemViewModel.CourseId))
                {
                    basket.BasketItem.Add(basketItemViewModel);
                }
            }
            else
            {
                basket = new BasketViewModel();

                basket.BasketItem.Add(basketItemViewModel);
            }

            await SaveOrUpdate(basket);
        }

        public Task<bool> ApplyDiscount(string discountCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelApplyDiscount()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteBasket()
        {
            var result = await httpClient.DeleteAsync("basket");
            return result.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> GetBasket()
        {
            var response = await httpClient.GetAsync("basket");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();

            return basketViewModel.Data;
        }

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await GetBasket();

            if(basket is null)
            {
                return false;
            }

            var deleteBasketItem = basket.BasketItem.Remove(basket.BasketItem.First(x=>x.CourseId == courseId));

            if (!deleteBasketItem)
            {
                return false;
            }

            if (!basket.BasketItem.Any())
            {
                basket.DiscountCode = null;
            }

            return await SaveOrUpdate(basket);


        }

        public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            var response = await httpClient.PostAsJsonAsync<BasketViewModel>("basket",basketViewModel);

            return response.IsSuccessStatusCode;
        }
    }
}
