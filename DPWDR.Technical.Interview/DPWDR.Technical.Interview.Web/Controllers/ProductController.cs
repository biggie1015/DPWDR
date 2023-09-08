using DPWDR.Technical.Interview.Data.Entities;
using DPWDR.Technical.Interview.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPWDR.Technical.Interview.Web.Controllers
{
   
    public class ProductController : Controller
    {

        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(DateTime? date, int? productId)
        {
            var products = _productService.GetProductsInStockAsync(date, productId);

            if (products != null)
            {
                return View(products);
            }
            else
            {
                
                return View(new List<Product>()); 
            }
        }
      

       
    }
}
