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

namespace BookStore.Web.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderItmeServices _orderItmeServices;
        private readonly OrderServices _orderServices;

        public OrderController(OrderItmeServices orderItmeServices, OrderServices orderServices) {
            _orderItmeServices = orderItmeServices;
            _orderServices = orderServices;
        }

        [Area("Admin")]
        // GET: OrderController
        public async Task<ActionResult> Index()
        {
            List<OrderListViewModel> allOrders = (await _orderServices.GetOrderListViewModelAsync()).ToList();

            return View(allOrders);

            // Edit Order:
            // #Process the order 
            //             alert
            //             change order status to Process 
            //              needs the carrier and TrackingNumber and ShippingDate
            //                     yes => ajax success find the carrier
            //                            Update the partial summary page
            //                                                           title of the order state
            //                                                           button of action from process to ship
            //                     
            //                     No => ajax fail alert 
            //                           ask for the carrier


            // 
            // #Cancel Order:
            //           1- from the form
            //           2- from the list
            //                       alert
            //                       change order status to cancel


        }


        // GET: OrderController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id <= 0)
                return NotFound();

            try
            {
                // Take the viewModel from db
                ManageOrderViewModel orderViewModel = new ManageOrderViewModel();

                orderViewModel.OrderItems = (await _orderItmeServices.GetOrderItemViewModelAsync()).ToList();

                return View(orderViewModel);
            }
            catch (Exception ex)
            {
                TempData["error"] = "حدث خطأ أثناء استرجاع الطلب للتعديل";
                return View("Error");
            }
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ManageOrderViewModel orderViewModel)
        {
            if (id <= 0)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    ClaimsIdentity claimsIdentity = (ClaimsIdentity)User?.Identity;
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);



                    TempData["success"] = "تم تحديث الطلب بنجاح!";

                    return RedirectToAction(nameof(Index));
                }
                catch
                {

                    TempData["error"] = " حدث خطأ أثناء تعديل الطلب";
                    return View("Error");
                }
            }
            else
            {
                // Take the viewModel from db
                orderViewModel = new ManageOrderViewModel();

                orderViewModel.OrderItems = (await _orderItmeServices.GetOrderItemViewModelAsync()).ToList();
                return View(orderViewModel);
            }

        }


        /*
         
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		public async Task<IActionResult> UpdateOrderDetails()
		{
			try
			{
				TbOrder? orderFromDb = await _unitOfWork.Order.GetByIdAsync(OrderViewModel.Order.Id);

				Mapper.Map(OrderViewModel, orderFromDb);

				_unitOfWork.Order.Update(orderFromDb);
				await _unitOfWork.SaveAsync();

				TempData["Success"] = "Order details updated successfully!";
				return RedirectToAction(nameof(Details), new { orderId = OrderViewModel.Order.Id });
			}
			catch (Exception ex)
			{
				TempData["Error"] = "An error occurred while updating order details. Please try again.";
				return RedirectToAction(nameof(Details), new { orderId = OrderViewModel.Order.Id });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		public async Task<IActionResult> StartProccessing()
		{
			try
			{
				_unitOfWork.Order.UpdateStatus(OrderViewModel.Order.Id, SD.StatusInProcess);
				await _unitOfWork.SaveAsync();

				TempData["Success"] = "Order status updated successfully!";
				return RedirectToAction(nameof(Details), new { orderId = OrderViewModel.Order.Id });
			}
			catch (Exception ex)
			{
				TempData["Error"] = "An error occurred while starting order processing. Please try again.";
				return RedirectToAction(nameof(Details), new { orderId = OrderViewModel.Order.Id });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		public async Task<IActionResult> ShipOrder()
		{
			try
			{
				var orderFromDb = await _unitOfWork.Order.GetByIdAsync(OrderViewModel.Order.Id);
				orderFromDb.TrackingNumber = OrderViewModel.Order.TrackingNumber;
				orderFromDb.Carrier = OrderViewModel.Order.Carrier;
				orderFromDb.OrderStatus = SD.StatusShipped;
				orderFromDb.ShippingDate = DateTime.Now;

				if (orderFromDb.PaymentStatus == SD.PaymentStatusDelayedPayment)
				{
					orderFromDb.PaymentDueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
				}

				_unitOfWork.Order.Update(orderFromDb);
				await _unitOfWork.SaveAsync();

				TempData["Success"] = "Order shipped successfully!";
				return RedirectToAction(nameof(Details), new { orderId = OrderViewModel.Order.Id });
			}
			catch (Exception ex)
			{
				TempData["Error"] = "An error occurred while shipping the order. Please try again.";
				return RedirectToAction(nameof(Details), new { orderId = OrderViewModel.Order.Id });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		public async Task<IActionResult> CancelOrder()
		{
			try
			{
				var orderFromDb = await _unitOfWork.Order.GetByIdAsync(OrderViewModel.Order.Id);

				if (orderFromDb.PaymentStatus == SD.PaymentStatusApproved)
				{
					var options = new RefundCreateOptions
					{
						Reason = RefundReasons.RequestedByCustomer,
						PaymentIntent = orderFromDb.PaymentIntentId,
					};

					var service = new RefundService();
					Refund refund = await service.CreateAsync(options);

					_unitOfWork.Order.UpdateStatus(orderFromDb.Id, SD.StatusCanceled, SD.StatusRefunded);
				}
				else
				{
					_unitOfWork.Order.UpdateStatus(orderFromDb.Id, SD.StatusCanceled, SD.StatusCanceled);
				}

				await _unitOfWork.SaveAsync();
				TempData["Success"] = "Order canceled successfully!";
				return RedirectToAction(nameof(Details), new { orderId = OrderViewModel.Order.Id });
			}
			catch (Exception ex)
			{
				TempData["Error"] = "An error occurred while canceling the order. Please try again.";
				return RedirectToAction(nameof(Details), new { orderId = OrderViewModel.Order.Id });
			}
		}
         
         
         
         */


    }
}
