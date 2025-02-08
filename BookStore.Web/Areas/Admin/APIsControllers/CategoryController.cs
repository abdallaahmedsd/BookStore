using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Areas.Admin.APIsControllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryServices _categoryServices;

        public CategoryController( CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { success = false, message = $"({id}) is an invalid Id" });

            try
            {
                Category? categoryModel = await _categoryServices.FindAsync(id);

                if (categoryModel == null)
                    return NotFound();

                await _categoryServices.DeleteAsync(id);

                return Ok(new { success = true, message = "Category deleted successfully!" });
            }
            catch (Exception ex)
            {
                // Log exception details (optional) for debugging
                return StatusCode(500, new { success = false, message = "An error occurred while processing your request.", error = ex.Message });
            }
        }
    }
}
