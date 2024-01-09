using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperMVC.Models
{
    public class PaginatedViewModel<T>
    {
        public IEnumerable<T> Data { get; set; }  // The paginated data to be displayed

        public int CurrentPage { get; set; }      // The current page number

        public int PageSize { get; set; }         // Number of items per page

        public int TotalItems { get; set; }       // Total number of items in the entire dataset

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);  // Calculated total number of pages
    }
}