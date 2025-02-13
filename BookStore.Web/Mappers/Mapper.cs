using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.Identity;
using BookStore.Models.ViewModels.Admin;
using BookStore.Models.ViewModels.Admin.Book;
using BookStore.Models.ViewModels.Admin.Order;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Customer.Book;
using BookStore.Models.ViewModels.Customer.Cart;
using BookStore.Models.ViewModels.Customer.OrderVM;
using BookStore.Utilties;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Web.Mappers
{
    public class Mapper
    {
        public static void Map(Book bookModel, AddEditBookViewModel bookViewModel)
        {
            bookViewModel.Id = bookModel.Id;
            bookViewModel.AuthorID = bookModel.AuthorID;
            bookViewModel.CategoryID = bookModel.CategoryID;
            bookViewModel.Title = bookModel.Title;
            bookViewModel.LanguageID = bookModel.LanguageID;
            bookViewModel.Price = bookModel.Price;
            bookViewModel.Description = bookModel.Description;
            bookViewModel.ISBA = bookModel.ISBA;
            bookViewModel.PublicationDate = bookModel.PublicationDate;
            bookViewModel.CoverImage = bookModel.CoverImage;
        }

        public static void Map(AddEditBookViewModel bookViewModel, Book bookModel)
        {
            bookModel.Id = bookViewModel.Id;
            bookModel.AuthorID = bookViewModel.AuthorID;
            bookModel.CategoryID = bookViewModel.CategoryID;
            bookModel.Title = bookViewModel.Title;
            bookModel.LanguageID = bookViewModel.LanguageID;
            bookModel.Price = bookViewModel.Price;
            bookModel.Description = bookViewModel.Description;
            bookModel.ISBA = bookViewModel.ISBA;
            bookModel.PublicationDate = bookViewModel.PublicationDate;
            bookModel.CoverImage = bookViewModel.CoverImage;
        }
      
        public static void Map(Category categoryModel, CategoryViewModel categoryViewModel)
        {
            categoryViewModel.Id = categoryModel.Id;
            categoryViewModel.Name = categoryModel.Name;
        }
        public static void Map(CategoryViewModel categoryViewModel ,Category categoryModel)
        {
            categoryModel.Id = categoryViewModel.Id;
            categoryModel.Name = categoryViewModel.Name;
        }

        public static void Map(AddEditAuthorViewModel authorViewModel, Author authorModel)
        {
            authorModel.Id = authorViewModel.Id;
            authorModel.Bio = authorViewModel.Bio;
            authorModel.FirstName = authorViewModel.FirstName;
            authorModel.LastName = authorViewModel.LastName;
            authorModel.NationalityID = authorViewModel.NationalityID;
            authorModel.Phone = authorViewModel.Phone;
            authorModel.Email = authorViewModel.Email;
            authorModel.ProfileImage = authorViewModel.ProfileImage;
        }
        public static void Map(Author authorModel, AddEditAuthorViewModel authorViewModel)
        {
            authorViewModel.Id = authorModel.Id;
            authorViewModel.Bio = authorModel.Bio;
            authorViewModel.FirstName = authorModel.FirstName;
            authorViewModel.LastName = authorModel.LastName;
            authorViewModel.NationalityID = authorModel.NationalityID;
            authorViewModel.Phone = authorModel.Phone;
            authorViewModel.Email = authorModel.Email;
            authorViewModel.ProfileImage = authorModel.ProfileImage;
        }

        public static void Map(BookDetailsViewModel bookDetailsModel, BookDetailsForCustomerViewModel BookDetailsForCustomerViewModel)
        {
            BookDetailsForCustomerViewModel.Id = bookDetailsModel.Id; ///////////solve this why sometime is null and sometimes not
            BookDetailsForCustomerViewModel.AuthorName = bookDetailsModel.AuthorName;
            BookDetailsForCustomerViewModel.CategoryName = bookDetailsModel.CategoryName;
            BookDetailsForCustomerViewModel.LanguageName = bookDetailsModel.LanguageName;
            BookDetailsForCustomerViewModel.Title = bookDetailsModel.Title;
            BookDetailsForCustomerViewModel.Price = bookDetailsModel.Price;
            BookDetailsForCustomerViewModel.Description = bookDetailsModel.Description;
            BookDetailsForCustomerViewModel.ISBA = bookDetailsModel.ISBA;
            BookDetailsForCustomerViewModel.PublicationDate = bookDetailsModel.PublicationDate;
            BookDetailsForCustomerViewModel.CoverImage = bookDetailsModel.CoverImage;
            BookDetailsForCustomerViewModel.TotalSellingQuantity = bookDetailsModel.TotalSellingQuantity;
        }

        public static void Map(AddToCartViewModel shoppingCartViewModel, ShoppingCard shoppingCart)
        {
            shoppingCart.BookID = shoppingCartViewModel.BookId;
            shoppingCart.Quantity = shoppingCartViewModel.Quantity;
            shoppingCart.UserID = shoppingCartViewModel.UserId;
            shoppingCart.SubTotal = shoppingCartViewModel.Quantity * shoppingCartViewModel.Price;
        }

        public static void Map(OrderItemViewModel cartItem, OrderItem orderItem)
        {
            orderItem.BookId = cartItem.BookID;
            orderItem.Quntity = cartItem.Quantity;
            orderItem.SubTotal = cartItem.SubTotal;
        }

        public static void Map(OrderSummaryViewModel orderViewModel, Order order)
        {
            order.CreatedDate = orderViewModel.CreatedDate;
            order.TotalAmoumt = orderViewModel.OrderTotalAmount;
            order.Status = (byte)OrderServices.enOrderStatus.Approved;
        }

        public static void Map(OrderSummaryViewModel orderViewModel, Shipping shipping)
        {
            shipping.CountryID = orderViewModel.CountryId;
            shipping.City = orderViewModel.City;
            shipping.Address = orderViewModel.Address;
            shipping.ZipCode = orderViewModel.ZipCode;
            shipping.EstimatedDelivery = orderViewModel.EstimatedDelivery;
        }

        public static void Map(Shipping shipping, ManageOrderViewModel manageOrderViewModel)
        {
            manageOrderViewModel.TrackingNumber = shipping?.TrackingNumber;
            manageOrderViewModel.EstimatedDelivery = (DateTime)shipping.EstimatedDelivery;
            manageOrderViewModel.ShippingDate = shipping.ShippingDate;
            manageOrderViewModel.City = shipping.City;
            manageOrderViewModel.Address = shipping.Address;
            manageOrderViewModel.ZipCode = shipping.ZipCode;
            manageOrderViewModel.Carrier = shipping.Carrier;
        }

        public static void Map(OrderSummaryViewModel orderViewModel, Payment payment)
        {
            payment.PaymentDate = orderViewModel.CreatedDate;
            payment.Amount = orderViewModel.OrderTotalAmount;
        }

        public static void Map(OrderDetailsViewModel orderDetailsViewModel, ManageOrderViewModel manageOrderViewModel)
        {
            manageOrderViewModel.Id = orderDetailsViewModel.Id;
            manageOrderViewModel.UserID = orderDetailsViewModel.UserID;
            manageOrderViewModel.CreatedDate = orderDetailsViewModel.CreatedDate;
            manageOrderViewModel.TotalAmoumt = orderDetailsViewModel.TotalAmoumt;
            manageOrderViewModel.CountryName = orderDetailsViewModel.CountryName;
            manageOrderViewModel.Status = _SetOrderStatus(orderDetailsViewModel.Status);
            manageOrderViewModel.City = orderDetailsViewModel.City;
            manageOrderViewModel.Address = orderDetailsViewModel.Address;
            manageOrderViewModel.ZipCode = orderDetailsViewModel.ZipCode;
            manageOrderViewModel.EstimatedDelivery = orderDetailsViewModel.EstimatedDelivery;
            manageOrderViewModel.FullName = orderDetailsViewModel.FullName;
            manageOrderViewModel.Email = orderDetailsViewModel.Email;
        }

        public static void Map(ManageOrderViewModel manageOrderViewModel, Order orderModel)
        {
            orderModel.CreatedDate = manageOrderViewModel.CreatedDate;
            orderModel.TotalAmoumt = manageOrderViewModel.TotalAmoumt;
            orderModel.Status = _SetOrderStatus(manageOrderViewModel.Status);
            orderModel.UserID = manageOrderViewModel.UserID;
        }

        public static void Map(ManageOrderViewModel manageOrderViewModel, Shipping shippingModel)
        {
            shippingModel.OrderID = manageOrderViewModel.Id;
            shippingModel.ShippingDate = manageOrderViewModel.ShippingDate;
            shippingModel.TrackingNumber = manageOrderViewModel.TrackingNumber;
            shippingModel.EstimatedDelivery = manageOrderViewModel.EstimatedDelivery;
            shippingModel.Carrier = manageOrderViewModel.Carrier;
            shippingModel.CountryID = 4; // ******************************
            shippingModel.City = manageOrderViewModel.City;
            shippingModel.Address = manageOrderViewModel.Address;
            shippingModel.ZipCode = manageOrderViewModel.ZipCode;
            shippingModel.Carrier = manageOrderViewModel.Carrier;
        }

        public static void Map(ManageOrderViewModel manageOrderViewModel, Payment payment)
        {
            payment.PaymentDate = manageOrderViewModel.CreatedDate;
            payment.Amount = manageOrderViewModel.TotalAmoumt;
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

        private static byte _SetOrderStatus(string Status)
        {
            switch (Status)
            {
                case SessionHelper.StatusApproved:
                    return 1;
                case SessionHelper.StatusInProcess:
                    return 2;
                case SessionHelper.StatusShipped:
                    return 3;
                case SessionHelper.StatusCanceled:
                    return 4;
                default:
                    return 0;
            }

        }
    }
}
