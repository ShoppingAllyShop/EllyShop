using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Models.Base
{
    public class PagingResponseBase
    {
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        // Số thứ tự của trang hiện tại
        public int PageNumber { get; set; } = 1;

        // Số lượng item mỗi trang
        public virtual int PageSize { get; set; } = 10;

        // Tổng số lượng item (để tính số trang)
        public int TotalItems { get; set; }

        // Tổng số trang
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        // Flag để kiểm tra có trang tiếp theo không
        public bool HasNextPage => PageNumber < TotalPages;

        // Flag để kiểm tra có trang trước đó không
        public bool HasPreviousPage => PageNumber > 1;
    }
}
