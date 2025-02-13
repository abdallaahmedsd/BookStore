using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace BookStore.Web.Areas.Admin.APIsControllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderItmeServices _orderItmeServices;
        private readonly ShippingServices _shippingServices;
        private readonly OrderServices _orderServices;
        public readonly PaymentServices _paymentServices;


        public OrderController(OrderItmeServices orderItmeServices,ShippingServices shippingServices, OrderServices orderServices,PaymentServices paymentServices)
        {
            _orderItmeServices = orderItmeServices;
            _shippingServices = shippingServices;
            _orderServices = orderServices;
            _paymentServices = paymentServices;
        }


        [HttpPost("cancel/{id:int}")]
        public async Task<ActionResult> Cancel(int id)
        {
            if (id <= 0)
                return BadRequest(new { success = false, message = $"({id}) رقم المعرف غير صالح" });

            try
            {
                List<OrderItem> orderItems = (await _orderItmeServices.GetAllItmesByOrderId(id)).ToList();

                // Delete all its orderItems
                foreach (OrderItem item in orderItems)
                {
                   await _orderItmeServices.DeleteAsync(item.Id);
                }

                int UserId = (await _orderServices.GetOrderByIdAsync(id)).UserID;

                // delete Order's shipping 
                //await _shippingServices.DeleteShippingByUserIdAsync(UserId); //fix

                 //delete Order's payment 
                //await _paymentServices.DeletePaymentByUserIdAsync(UserId); //fix

                // Cancel Order
                await _orderServices.UpdateStatus(id,OrderServices.enOrderStatus.Cancel);

                return Ok(new { success = true, message = "تم إلغاء الطلب بنجاح!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "حدث خطأ أثناء معالجة طلبك.", error = ex.Message });
            }
        }
    }
}
