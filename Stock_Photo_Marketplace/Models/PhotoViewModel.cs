using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Stock_Photo_Marketplace.ViewModels
{
        public class PhotoViewModel
        {
            public int PhotoID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int CategoryID { get; set; }
            public IEnumerable<SelectListItem> Categories { get; set; }

            public string FilePath { get; set; }

            public IFormFile? PhotoFile { get; set; } // Add for file upload
        }
}
