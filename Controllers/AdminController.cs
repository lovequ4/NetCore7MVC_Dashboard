using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dashboard.Context;
using Dashboard.Service;
using Dashboard.Models;

namespace Dashboard.Controllers
{ 
    public class AdminController : Controller
    {
        private readonly AppDBContext _dbContext;
        public AdminController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
             // Assuming you have an Entity Framework DbContext set up
            var data = _dbContext.products.ToList(); // Replace YourDataEntity with your actual entity name

            // Transform your data into the format needed for your chart (e.g., a list of tuples or a custom class)
            var chartData = data.Select(item => new Tuple<string, int>(item.ProductName, item.Quantity)).ToList();

            // Pass the chartData to the view
            ViewBag.ChartData = chartData;

            
            return View();
        }


        //Product GET//
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var products = _dbContext.products.ToList();
            
            return View(products);
        }

        //Product CREATE//
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
           bool productExists = _dbContext.products.Any(p => p.ProductName == product.ProductName);

            if (productExists)
            {
                ViewBag.notice = "Product already exists";
                return View(product); 
            }

            _dbContext.products.Add(product);
            _dbContext.SaveChanges(); 

            return View();
        }

        //Product Put//
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            Product product = _dbContext.products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); 
            }

            return View(product); 
        }

        [HttpPost]
        public IActionResult EditProduct(int id, Product updatedProduct)
        {
           var Product = _dbContext.products.FirstOrDefault(p => p.Id == id);

            if(Product != null)
            {   
                Product.Id = id;
                Product.ProductName = updatedProduct.ProductName;
                Product.Brand = updatedProduct.Brand;
                Product.Price = updatedProduct.Price;
                Product.Quantity = updatedProduct.Quantity;
                Product.EditDate = updatedProduct.EditDate;
                Product.EditTime = updatedProduct.EditTime;
                
                _dbContext.SaveChanges();
                
                // return Redirect("/Admin/GetAllProduct/");
            }
        
            return View(Product); 
        }


        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            var Product = _dbContext.products.FirstOrDefault(p => p.Id == id);

            if(Product != null)
            {
                _dbContext.Remove(Product);
                _dbContext.SaveChanges(); 

                return Redirect("/Admin/GetAllProduct"); 

            }else{
                return NotFound();
            }
        }
    }
}