using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_Catalog
{
    public partial class ProductImages
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ProductColor: hình đại diện khi chọn màu sản phẩm
        /// </summary>
        public string? Type { get; set; }
        public string? Picture { get; set; }
        public string? ProductId { get; set; }
        public int? ImageIndex { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? ColorId { get; set; }
        /// <summary>
        /// dung de set mau mac dinh cho prodcutdetail
        /// </summary>
        public bool? DefaultColor { get; set; }

        public virtual Color? Color { get; set; }
        public virtual Product? Product { get; set; }
    }
}
