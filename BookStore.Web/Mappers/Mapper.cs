﻿using BookStore.BusinessLogic.Services;
using BookStore.Models.Entities;
using BookStore.Models.Identity;
using BookStore.Models.ViewModels.Admin;
using BookStore.Models.ViewModels.Admin.Book;
using BookStore.Models.ViewModels.Book;
using BookStore.Models.ViewModels.Customer.Book;
using BookStore.Models.ViewModels.Customer.Cart;
using BookStore.Models.ViewModels.Customer.OrderVM;
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

        //Customer
        public static void Map(BookDetailsViewModel bookDetailsModel, BookDetailsForCustomerViewModel BookDetailsForCustomerViewModel)
        {
            BookDetailsForCustomerViewModel.Id = bookDetailsModel.Id;
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
            order.CreatedDate = DateTime.Now;
            order.TotalAmoumt = orderViewModel.OrderTotalAmount;
            order.Status = 1;
        }
        public static void Map(OrderSummaryViewModel orderViewModel, Shipping shipping)
        {
            shipping.ShippingAddress = $"{orderViewModel.CountryName} - {orderViewModel.State} - {orderViewModel.Address} - {orderViewModel.ZipCode}";
            shipping.ShippingDate = DateTime.Now; //*****************
            shipping.TrackingNumber = "mmmmmmm"; //***********************
            shipping.EstimatedDelivery = DateTime.Now.AddDays(10);
            shipping.Status = (int)ShippingServices.enShippingStatus.Ordered;
        }



        //public static void Map(ApplicationUser user, TbOrder order)
        //{
        //    order.Name = user.Name;
        //    order.PhoneNumber = user.PhoneNumber;
        //    order.City = user.AddressInfo.City;
        //    order.StreetAddress = user.AddressInfo.StreetAddress;
        //    order.State = user.AddressInfo.State;
        //    order.State = user.AddressInfo.State;
        //    order.PostalCode = user.AddressInfo.PostalCode;
        //}

        //public static void Map(OrderViewModel orderViewModel, TbOrder order)
        //{
        //    order.Name = orderViewModel.Order.Name;
        //    order.StreetAddress = orderViewModel.Order.StreetAddress;
        //    order.PhoneNumber = orderViewModel.Order.PhoneNumber;
        //    order.State = orderViewModel.Order.State;
        //    order.City = orderViewModel.Order.City;

        //    if (!string.IsNullOrEmpty(orderViewModel.Order.Carrier))
        //        order.Carrier = orderViewModel.Order.Carrier;

        //    if (!string.IsNullOrEmpty(orderViewModel.Order.TrackingNumber))
        //        order.TrackingNumber = orderViewModel.Order.TrackingNumber;
        //}
    }
}
