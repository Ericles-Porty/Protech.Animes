using System.ComponentModel.DataAnnotations;

public class PaginationParams
{
    [Range(1, int.MaxValue)]
    public int? Page { get; set; }

    [Range(1, int.MaxValue)]
    public int? PageSize { get; set; }
}
