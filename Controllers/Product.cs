using Microsoft.AspNetCore.Mvc;
using ProjectPitch4.Models;
using ProjectPitch4.Services;
using MongoDB.Driver;
using System.Linq;
using MongoDB.Bson;

namespace ProjectPitch4.Controllers
{
    /// <summary>
    /// Controller for managing products.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Product : ControllerBase
    {
        private readonly MongoService _context;

        private readonly ILogger<Product> _logger;

        /// <summary>
        /// Constructor for the Product controller.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="context">The MongoService context.</param>
        public Product(ILogger<Product> logger, MongoService context)
        {
            _logger = logger;
            _context = context;
        }


        // [HttpGet("ProductList")]
        // public IEnumerable<ProductDescription> Get()
        // {
        // return _context._productDescriptions.Find(x => true).Limit(5).ToList();
        // }


        /// <summary>
        /// Gets a paginated list of products.
        /// </summary>
        /// <remarks>
        /// pageNumber : Starts from 1 and each page contains 50 records
        /// </remarks>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>An IEnumerable of ProductDescription.</returns>
        [HttpGet("ProductList")]
        public IEnumerable<ProductDescription> Get(int pageNumber = 1)
        {
            int pageSize = 100; // Fixed page size
            int skip = (pageNumber - 1) * pageSize;
            return _context._productDescriptions.AsQueryable().Skip(skip).Take(pageSize).ToList();
        }



        /// <summary>
        /// Retrieves product descriptions by product ID.
        /// </summary>
        /// <remarks>
        /// This method retrieves a list of product descriptions that match the specified product ID.
        /// </remarks>
        /// <param name="productID">The ID of the product to retrieve.</param>
        /// <returns>An IEnumerable of ProductDescription matching the provided product ID.</returns>
        [HttpGet("GetByProductID/{productID}")]
        public IEnumerable<ProductDescription> GetByProductID(Int32 productID)
        {
            return _context._productDescriptions.Find(x => x.ProductID == productID).ToList();
        }



        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <remarks>
        /// This method adds a new product to the database if the product does not already exist.
        /// If a product with the same ID already exists, it returns an error message indicating that the product already exists.
        /// </remarks>
        /// <param name="payload">The product data to add.</param>
        /// <returns>
        /// IActionResult representing the result of the operation.
        /// - If the product is successfully added, returns an Ok result with a custom response object containing a success message and the added product.
        /// - If the product already exists, returns a BadRequest result with an error message indicating that the product already exists.
        /// </returns>
        [HttpPost("AddProduct")]
        public IActionResult Post(ProductDescription_Base payload)
        {
            // Check if the StateProvinceName already exists
            var existingProduct = _context._productDescriptions.Find(r => r.ProductID == payload.ProductID).FirstOrDefault();

            if (existingProduct != null)
            {
                // If the StateProvinceName already exists, return an error message
                return BadRequest("Product already exists.");
            }

            // If everything is fine, proceed with adding the new product
            var product = new ProductDescription
            {
                ProductID = payload.ProductID,
                Name = payload.Name,
                ProductModel = payload.ProductModel,
                CultureID = payload.CultureID,
                Description = payload.Description
            };

            _context._productDescriptions.InsertOne(product);

            // Create a custom response object
            var response = new
            {
                Message = "Product successfully added.",
                AddedProduct = product
            };

            return Ok(response);
        }



        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <remarks>
        /// This method updates the fields of an existing product in the database with the provided product ID.
        /// If the provided ID is invalid, it returns a BadRequest result with an error message indicating an invalid ID format.
        /// If the product with the provided ID is not found, it returns a NotFound result with an error message indicating that the product was not found.
        /// If the product is successfully updated, it returns an Ok result with a success message indicating that the product was successfully updated.
        /// </remarks>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="updatedProduct">The updated product data.</param>
        /// <returns>
        /// IActionResult representing the result of the operation.
        /// - If the ID format is invalid, returns a BadRequest result with an error message.
        /// - If the product is not found, returns a NotFound result with an error message.
        /// - If the product is successfully updated, returns an Ok result with a success message.
        /// </returns>
        [HttpPut("UpdateProduct/{id}")]
        public IActionResult Update(string id, ProductDescription_Base updatedProduct)
        {
            // Check if the provided id is valid
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest("Invalid id format.");
            }

            // Find the product to be updated
            var existingProduct = _context._productDescriptions.Find(r => r._id == id).FirstOrDefault();
            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            // Update the fields of the existing product
            existingProduct.ProductID = updatedProduct.ProductID;
            existingProduct.Name = updatedProduct.Name;
            existingProduct.ProductModel = updatedProduct.ProductModel;
            existingProduct.CultureID = updatedProduct.CultureID;
            existingProduct.Description = updatedProduct.Description;

            // Replace the existing product with the updated one in the database
            var result = _context._productDescriptions.ReplaceOne(r => r._id == id, existingProduct);

            if (result.ModifiedCount > 0)
            {
                // Product successfully updated
                return Ok("Product successfully updated.");
            }
            else
            {
                // Product not found or not updated
                return NotFound("Product not found or not updated.");
            }
        }



        /// <summary>
        /// Deletes a product using Id.
        /// </summary>
        /// <remarks>
        /// This method deletes a product from the database based on the provided ID.
        /// If the product with the provided Id is found and successfully deleted, it returns an Ok result with a success message.
        /// If the product with the provided Id is not found or not deleted, it returns a NotFound result with an error message.
        /// ID is the generated string of the product.
        /// </remarks>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>
        /// IActionResult representing the result of the operation.
        /// - If the product is successfully deleted, returns an Ok result with a success message.
        /// - If the product is not found or not deleted, returns a NotFound result with an error message.
        /// </returns>
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _context._productDescriptions.DeleteOne(x => x._id == id);

            if (result.DeletedCount > 0)
            {
                // Product successfully deleted
                return Ok("Product successfully deleted.");
            }
            else
            {
                // Product not found or not deleted
                return NotFound("Product not found or not deleted.");
            }
        }
    }
}
