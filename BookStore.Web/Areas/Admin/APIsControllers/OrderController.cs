using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BookStore.Web.Areas.Admin.APIsControllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderItmeServices _orderItmeServices;
        private readonly ShippingServices _shippingServices;
        private readonly OrderServices _orderServices;

        public OrderController(OrderItmeServices orderItmeServices,ShippingServices shippingServices, OrderServices orderServices)
        {
            _orderItmeServices = orderItmeServices;
            _shippingServices = shippingServices;
            _orderServices = orderServices;
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
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

                // delete the shipping for this order
                //await _shippingServices.delete(id);

                // Delete the actual Order
                await _orderItmeServices.DeleteAsync(id);

                return Ok(new { success = true, message = "تم حذف الطلب بنجاح!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "حدث خطأ أثناء معالجة طلبك.", error = ex.Message });
            }
        }
    }
}
