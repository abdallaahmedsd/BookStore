using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.APIsControllers
{
    [Route("api/admin/[controller]")]

    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthorController(AuthorService authorService, IWebHostEnvironment webHostEnvironment)
        {
            _authorService = authorService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { success = false, message = $"({id}) رقم المعرف غير صالح" });

            try
            {
                Author? author = await _authorService.FindAsync(id);

                if (author == null)
                    return NotFound(new { success = false, message = $"لا يوجد مؤلف برقم المعرف ({id})" });

                _DeleteAuthorImage(author.Id);

                await _authorService.DeleteAsync(id);

                return Ok(new { success = true, message = "تم حذف المؤلف بنجاح!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "حدث خطأ أثناء معالجة طلبك.", error = ex.Message });
            }
        }


        private void _DeleteAuthorImage(int authorId)
        {
            // Delete author image
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string authorPath = @"uploads\images\authors\author-" + authorId;
            string finalPath = Path.Combine(wwwRootPath, authorPath);

            if (Directory.Exists(finalPath))
            {
                //*
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
