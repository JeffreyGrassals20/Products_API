using Microsoft.AspNetCore.Mvc;
using Products_API.Models;

namespace Products_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> Products = new();

        private static int _currentid = 1;

        private static int GenerateId()
        {
            return _currentid++;
        }

        // GET: api/Products
        [HttpGet()]
        public List<Product> Get([FromQuery]string? name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                var productsByName = Products.FindAll(p => p.Name.Contains(name));
                return productsByName;
            }
            return Products;
        }
        
        // POST: api/Products
        [HttpPost]
        public int Post(Product value)
        {
            var newObject = new Product
            {
                Id = GenerateId(),
                Name = value.Name,
                Description = value.Description,
                Price = value.Price,
                Tax = value.Tax
            };
            
            Products.Add(newObject);
            return newObject.Id;
        }

        // PUT: api/Products
        [HttpPut()]
        public bool Put(Product value)
        {
          var index =  Products.FindIndex(p => p.Id == value.Id);

          if (index == -1)
          {
              return false;
          }
          else
          {
              Products[index] = value;
              return true;
          }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var deletedItem = Products.RemoveAll(p => p.Id == id);
            return deletedItem > 0;
        }
    }
}
