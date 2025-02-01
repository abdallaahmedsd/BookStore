using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class RecentlyPublishedBookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public decimal Price { get; set; }
        public string? CoverImage { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
