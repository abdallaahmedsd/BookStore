using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore.Web.Areas.Admin.APIsControllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly BookServices _bookService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public BookController(BookServices bookService, IWebHostEnvironment webHostEnvironment)
        {
            _bookService = bookService;
            _webHostEnvironment = webHostEnvironment;
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

                _DeleteBookImages(book.Id);

                await _bookService.DeleteAsync(id);

                return Ok(new { success = true, message = "تم حذف الكتاب بنجاح!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "حدث خطأ أثناء معالجة طلبك.", error = ex.Message });
            }
        }

        private void _DeleteBookImages(int bookId)
        {
            // Delete book images
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string bookPath = @"uploads\images\books\book-" + bookId;
            string finalPath = Path.Combine(wwwRootPath, bookPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePathes = Directory.GetFiles(finalPath);
                foreach (string filePath in filePathes)
                {
                    System.IO.File.Delete(filePath);
                }

                Directory.Delete(finalPath, true);
            }
        }

    }
}
