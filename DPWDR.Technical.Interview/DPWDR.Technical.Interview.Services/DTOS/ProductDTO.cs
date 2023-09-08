using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DPWDR.Technical.Interview.Services.DTOS
{
    public class ProductDTO
    {
        public string? Title { get; set; }

     
        public decimal Price { get; set; }

    
        public string? Description { get; set; }

       
        public string? Category { get; set; }

      
        public string? Image { get; set; }
    }
}
