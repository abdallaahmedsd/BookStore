using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Web.Areas.Customer.APIsControllers
{

    [Route("api/customer/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ShoppingCartServices _shoppingCartServices;

        public CartController(ShoppingCartServices shoppingCartServices)
        {
            _shoppingCartServices = shoppingCartServices;
        }

        // DELETE: api/customer/Cart/item/{id}
        [HttpDelete("item/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0 )
                return BadRequest(new { success = false, message = $"({id}) رقم المعرف غير صالح" });

            try
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);


                await _shoppingCartServices.DeleteAsync(id, userId);


                return Ok(new { success = true, message = "تم حذف العنصر من السلة" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "حدث خطأ أثناء معالجة طلبك.", error = ex.Message });
            }
        }

        // DELETE: api/customer/Cart/cart
        [HttpDelete("cart")]
        public async Task<ActionResult> Delete()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                    //ShoppingCard cart = await _shoppingCartServices.FindAsync(userId);
                    //if (cart == null)
                    //    return NotFound(new { success = false, message = $"لا يوجد عناصر في السلة ! " });


                    await _shoppingCartServices.DeleteCustomerItemsAsync(userId);

                    return Ok(new { success = true, message = "تم حذف السلة بالكامل" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { success = false, message = "حدث خطأ أثناء معالجة طلبك.", error = ex.Message });
                }
            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

        }
    }
}
