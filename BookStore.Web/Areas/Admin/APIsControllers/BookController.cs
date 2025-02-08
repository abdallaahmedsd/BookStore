using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.APIsControllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        public readonly BookServices _bookService;


        public BookController(BookServices bookService)
        {
            _bookService = bookService;
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { success = false, message = $"({id}) رقم المعرف غير صالح" });

            try
            {
                Book? book = await _bookService.FindAsync(id);

                if (book == null)
                    return NotFound(new { success = false, message = $"لا يوجد كتاب برقم المعرف ({id})" });

                await _bookService.DeleteAsync(id);

                return Ok(new { success = true, message = "تم حذف الكتاب بنجاح!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "حدث خطأ أثناء معالجة طلبك.", error = ex.Message });
            }
        }

    }
}
