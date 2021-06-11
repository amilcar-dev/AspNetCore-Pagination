using Pagination.Enums;

namespace Pagination.Models
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string SearchText { get; set; }

        // Ascending or Descending
        public OrderBy? OrderBy { get; set; }

        // The name of property you want order by
        public string PropertyName { get; set; }

        public PaginationFilter(int pageNumber, int pageSize, OrderBy orderBy, string propertyName, string searchText)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : pageSize;
            OrderBy = orderBy;
            PropertyName = propertyName;
            SearchText = searchText;
        }
    }
}
