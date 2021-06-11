namespace Pagination.Models
{
    public class PagedResponse<T>
    {
        public T Data { get; set; }
        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public PagedResponse(T data, int totalPages, int totalRecords)
        {
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            Data = data;
        }
    }
}
