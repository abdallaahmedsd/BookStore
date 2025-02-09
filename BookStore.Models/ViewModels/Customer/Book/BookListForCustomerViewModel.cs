using BookStore.Models.ViewModels.Admin;
using BookStore.Models.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.ViewModels.Customer.Book
{
    public class BookListForCustomerViewModel
    {
        public  List<BookListViewModel> allBooks { get; set; }

        public string? filterSearch { get; set; } = "";
    }
}
