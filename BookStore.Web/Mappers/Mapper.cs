using BookStore.Models.Entities;
using BookStore.Models.Identity;
using BookStore.Models.ViewModels.Admin;
using BookStore.Models.ViewModels.Admin.Book;
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

        //public static void Map(Book bookModel, BookDetailsForAdminViewModel bookDetailsViewModel)
        //{
        //    bookDetailsViewModel.Id = bookModel.Id;
        //    bookDetailsViewModel.Title = bookModel.Title;
        //    bookDetailsViewModel.Description = bookModel.Description;
        //    bookDetailsViewModel.Price = bookModel.Price;
        //}


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
