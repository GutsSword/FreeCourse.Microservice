using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.FakePayment;
using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService basketService;
        private readonly IPaymentService paymentService;
        private readonly HttpClient httpClient;
        private readonly ISharedIdentityService identityService;

        public OrderService(IBasketService basketService, IPaymentService paymentService, HttpClient httpClient, ISharedIdentityService identityService)
        {
            this.basketService = basketService;
            this.paymentService = paymentService;
            this.httpClient = httpClient;
            this.identityService = identityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoViewModel checkoutInfoViewModel)
        {
            var basket = await basketService.GetBasket();

            var payment = new PaymentInfoViewModel()
            {
                CardName = checkoutInfoViewModel.CardName,
                CardNumber = checkoutInfoViewModel.CardNumber,
                CVV = checkoutInfoViewModel.CVV,
                Expiration = checkoutInfoViewModel.Expiration,

                TotalPrice = basket.TotalPrice,
            };

            var responsePayment = await paymentService.ReceivePayment(payment);

            if (!responsePayment)
            {
                return new OrderCreatedViewModel
                {
                    Error = "Ödeme alınamadı.",
                    IsSuccessful = false
                };
            }    

            var orderCreateInput = new OrderCreateViewModel()
            {
                BuyerId = identityService.GetUserId,
                Address = new AddressCreateViewModel
                {
                    District = checkoutInfoViewModel.District,
                    Line = checkoutInfoViewModel.Line,
                    Province = checkoutInfoViewModel.Province,
                    Street = checkoutInfoViewModel.Street,
                    ZipCode = checkoutInfoViewModel.ZipCode
                },               
            };

            basket.BasketItem.ForEach(b =>
            {
                var orderItem = new OrderItemCreateViewModel()
                {
                    ProductId = b.CourseId,
                    ProductName = b.CourseName,
                    Price = b.GetCurrentPrice,
                    PictureUrl = "Herhangi bir görsel bulunmamaktadır",
                };
                orderCreateInput.OrderItems.Add(orderItem);
            });

            var response = await httpClient.PostAsJsonAsync<OrderCreateViewModel>("orders",orderCreateInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel()
                {
                    Error = "Sipariş Oluşturulamadı.",
                    IsSuccessful = false,
                };
            }

            var orderCreatedViewModel= await response.Content.ReadFromJsonAsync<OrderCreatedViewModel>();

            orderCreatedViewModel.IsSuccessful = true;

            basketService.DeleteBasket();

            return orderCreatedViewModel;

        }

        public async Task<List<OrderViewModel>> GetAllOrders()
        {
            var response = await httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");
            return response.Data;
        }

        public Task SuspendOrder(CheckoutInfoViewModel checkoutInfoViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
