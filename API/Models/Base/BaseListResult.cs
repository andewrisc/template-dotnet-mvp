
namespace API.Models.Base;

public class BaseListResult<T>
{
    public List<T> Items { get; set; } 
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public long TotalRecords { get; set; }
    public int TotalPages { get; set; }

    public BaseListResult(List<T> items, long totalRecords, int pageNumber, int pageSize)
    {
        Items = items;
        TotalRecords = totalRecords;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
    public BaseListResult()
    {
        Items = new List<T>();
    }
}   
