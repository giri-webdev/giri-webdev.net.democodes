using giri_webdev_livedemo.Models.CodeFirst;
using System.Collections.Generic;

namespace giri_webdev_livedemo.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }

        public int TotalRecordCount { get; set; }
    }
}