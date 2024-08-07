using FreeCourse.Web.Models.Orders;

namespace FreeCourse.Web.Services.Concrete
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoViewModel checkoutInfoViewModel);

        // Asenkron
        Task SuspendOrder(CheckoutInfoViewModel checkoutInfoViewModel);

        Task<List<OrderViewModel>> GetAllOrders();
    }
}
