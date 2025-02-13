using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels.Admin.Book;
using BookStore.Models.ViewModels.Admin;
using BookStore.Models.ViewModels.Admin.Order;
using BookStore.Web.Mappers;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using BookStore.Utilties;

namespace BookStore.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class OrderController : Controller
    {
        private readonly OrderItmeServices _orderItmeServices;
        private readonly OrderServices _orderServices;
        private readonly ShippingServices _shippingServices;
        private readonly CountryService _countryService;
        private readonly PaymentServices _paymentServices;

        public OrderController(OrderItmeServices orderItmeServices, OrderServices orderServices,ShippingServices shippingServices,CountryService countryService,PaymentServices paymentServices) {
            _orderItmeServices = orderItmeServices;
            _orderServices = orderServices;
            _shippingServices = shippingServices;
            _countryService = countryService;
            _paymentServices = paymentServices;
        }

        public async Task<ActionResult> Index()
        {
            List<OrderListViewModel> allOrders = (await _orderServices.GetOrderListViewModelAsync()).ToList();

            return View(allOrders);
        }


        public async Task<ActionResult> Edit(int id)
        {
            // Should have the shipping details also with it and make them take null in OrderDetailsViewModel

            if (id <= 0)
                return NotFound();

            try
            {
                OrderDetailsViewModel orderDetailsViewModel = await _orderServices.GetOrderDetailsViewModleByOrderId(id);

                ManageOrderViewModel manageOrderViewModel = new ManageOrderViewModel();
                manageOrderViewModel.OrderItems = (await _orderItmeServices.GetOrderItemViewModelAsync()).ToList();
                Mapper.Map(orderDetailsViewModel, manageOrderViewModel);

                // will change this later
                Shipping shippingModel = await _shippingServices.FindByOrderIdAsync(id);
                Mapper.Map(shippingModel,manageOrderViewModel);


                return View(manageOrderViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "حدث خطأ أثناء استرجاع الطلب للتعديل";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ManageOrderViewModel manageOrderViewModel)
        {
            if (id <= 0)
                return NotFound();

                try
                {
                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);

                    Order orderModel =await _orderServices.GetOrderByIdAsync(id);
                    Mapper.Map(manageOrderViewModel, orderModel);
                    await _orderServices._UpdateAsync(orderModel);


                    Shipping shippingModel = await _shippingServices.FindByOrderIdAsync(id);
                    Mapper.Map(manageOrderViewModel, shippingModel);
                    await _shippingServices.UpdateAsync(shippingModel);


                //Payment paymentModel = await _paymentServices.GetPaymentByOrderIdAsync(id);
                //Mapper.Map(manageOrderViewModel, paymentModel);
                //await _paymentServices.UpdateAsync(paymentModel);


                TempData["success"] = "تم تحديث الطلب بنجاح!";
                    return RedirectToAction(nameof(Edit), new {id =id });

                }
                catch
                {

                    TempData["error"] = " حدث خطأ أثناء تعديل الطلب";
                    return RedirectToAction(nameof(Edit), manageOrderViewModel);
                }

        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> StartProcessing(ManageOrderViewModel manageOrderViewModel)
		{
			try
			{
                await _orderServices.UpdateStatus(manageOrderViewModel.Id, OrderServices.enOrderStatus.Process);
                manageOrderViewModel.Status = _SetOrderStatus((await _orderServices.GetOrderByIdAsync(manageOrderViewModel.Id)).Status); // 

                TempData["success"] = "تم تحديث الطلب بنجاح!";
                return RedirectToAction(nameof(Edit), manageOrderViewModel);
            }
            catch (Exception ex)
			{
                TempData["error"] = " حدث خطأ أثناء تعديل الطلب";
                return RedirectToAction(nameof(Edit), manageOrderViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShipOrder( ManageOrderViewModel manageOrderViewModel)
        {
            try
            {
                await _orderServices.UpdateStatus(manageOrderViewModel.Id, OrderServices.enOrderStatus.Shipped);
                manageOrderViewModel.Status = _SetOrderStatus((await _orderServices.GetOrderByIdAsync(manageOrderViewModel.Id)).Status); 

                TempData["success"] = "تم تحديث الطلب بنجاح!";
                //return View("Edit", new { orderId = manageOrderViewModel.Id });
                return RedirectToAction(nameof(Edit), manageOrderViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = " حدث خطأ أثناء تعديل الطلب";
                return RedirectToAction(nameof(Edit), manageOrderViewModel);
            }
        }

        private static string _SetOrderStatus(byte Status)
        {
            switch (Status)
            {
                case 1:
                    return SessionHelper.StatusApproved;
                case 2:
                    return SessionHelper.StatusInProcess;
                case 3:
                    return SessionHelper.StatusShipped;
                case 4:
                    return SessionHelper.StatusCanceled;
                default:
                    return "غير معروف";
            }

        }
    }
}
