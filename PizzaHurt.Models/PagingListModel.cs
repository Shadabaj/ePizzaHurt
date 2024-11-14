using X.PagedList;

namespace PizzaHurt.Models
{

    public class PagingListModel<T> where T : class
    {

        public StaticPagedList<T> Data { get; set; }
        public int TotalRows { get; set; }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }

        public bool ShowPrevious => Page > 1; //It returns true if the current page (Page) is greater than 1,

        public bool ShowNext => Page < TotalPages;  
        // It returns true if the current page (Page) is less than the total number of pages (TotalPages), indicating that there are pages following the current one.

        public bool ShowFirst => Page != 1; 
        //It returns true if the current page(Page) is not the first page, meaning it's greater than 1.

        public bool ShowLast => Page != TotalPages; 
        //if the current page (Page) is not the last page, meaning it's not equal to the total number of pages (TotalPages).




    }
}
