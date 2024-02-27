using Microsoft.AspNetCore.Mvc;
using ProjectPitch4.Models;
using ProjectPitch4.Services;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ProjectPitch4.Controllers
{
    /// <summary>
    /// Controller for managing regions.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly MongoService _context;

        private readonly ILogger<RegionController> _logger;

        /// <summary>
        /// Constructor for the RegionController class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="context">The MongoService context.</param>
        public RegionController(ILogger<RegionController> logger, MongoService context)
        {
            _logger = logger;
            _context = context;
        }

        // [HttpGet("RegionList")]
        // public IEnumerable<Region> Get()
        // {
        // return _context._regions.Find(x=>true).Limit(5).ToList();
        // }



        /// <summary>
        /// Retrieves a paginated list of regions.
        /// </summary>
        /// <remarks>
        /// This method retrieves a paginated list of regions from the database.
        /// The page size is fixed at 50 records per page.
        /// </remarks>
        /// <param name="pageNumber">The page number to retrieve. Default is 1.</param>
        /// <returns>
        /// An IEnumerable of Region objects representing the paginated list of regions.
        /// </returns>
        [HttpGet("RegionList")]
        public IEnumerable<Region> Get(int pageNumber = 1)
        {
            int pageSize = 50; // Fixed page size
            int skip = (pageNumber - 1) * pageSize;
            return _context._regions.AsQueryable().Skip(skip).Take(pageSize).ToList();
        }


        /// <summary>
        /// Retrieves a region using Id.
        /// </summary>
        /// <remarks>
        /// This method retrieves a region from the database based on the provided ID.
        /// </remarks>
        /// <param name="id">The ID of the region to retrieve.</param>
        /// <returns>
        /// A Region object representing the region with the provided ID.
        /// If no region is found with the provided ID, returns null.
        /// </returns>
        [HttpGet("GetRegion/{id}")]
        public Region Get(string id)
        {
            return _context._regions.Find(x => x._id == id).FirstOrDefault();
        }



        /// <summary>
        /// Retrieves regions using state province code.
        /// </summary>
        /// <remarks>
        /// This method retrieves regions from the database based on the provided state/province code.
        /// </remarks>
        /// <param name="stateProvinceCode">The state/province code to filter regions by.</param>
        /// <returns>
        /// An IEnumerable of Region objects representing the regions with the provided state/province code.
        /// </returns>
        [HttpGet("GetByProvinceCode/{stateProvinceCode}")]
        public IEnumerable<Region> GetByProvinceCode(string stateProvinceCode)
        {
            return _context._regions.Find(x => x.StateProvinceCode == stateProvinceCode).ToList();
        }



        // [HttpGet("FixProvinceCode")]
        // public IActionResult FixProvinceCode()
        // <summary>
        // [HttpGet("FixProvinceCode")]
        // </summary> _context._regions.Find(x => true).Project(x => new { x.StateProvinceCode, x._id }).ToList();
        // foreach (var region in regions)
        // {
        // _context._regions.UpdateOne(x => x._id == region._id, Builders<Region>
        // .Update
        // .Set(e => e.StateProvinceCode, region.StateProvinceCode.Trim()));
        // }

        // return Ok(regions.Count);

        // }

        // [HttpPost("AddRegion")]
        // public IActionResult Post(Region_Base payload)
        // {
        // var region = (Region)payload;
        // _context._regions.InsertOne(region);
        // return Ok();
        // }



        /// <summary>
        /// Adds a new region
        /// </summary>
        /// <remarks>
        /// This method adds a new region to the database based on the provided region information.
        /// </remarks>
        /// <param name="payload">The payload containing the information of the region to be added.</param>
        /// <returns>
        /// An IActionResult representing the outcome of the operation.
        /// </returns>
        [HttpPost("AddRegion")]
        public IActionResult Post(Region_Base payload)
        {
            // Check if the StateProvinceName already exists
            var existingRegion = _context._regions.Find(r => r.StateProvinceName == payload.StateProvinceName).FirstOrDefault();

            if (existingRegion != null)
            {
                // If the StateProvinceName already exists, return an error message
                return BadRequest("Province name already exists.");
            }

            // If everything is fine, proceed with adding the new region
            var region = new Region
            {
                StateProvinceID = payload.StateProvinceID,
                StateProvinceCode = payload.StateProvinceCode,
                IsOnlyStateProvinceFlag = payload.IsOnlyStateProvinceFlag,
                StateProvinceName = payload.StateProvinceName,
                TerritoryID = payload.TerritoryID,
                CountryRegionCode = payload.CountryRegionCode,
                CountryRegionName = payload.CountryRegionName
            };

            _context._regions.InsertOne(region);

            // Create a custom response object
            var response = new
            {
                AddedRegion = region,
                Message = "Region successfully added."
            };

            // Return the custom response object with status code 200
            return Ok(response);
        }



        /// <summary>
        /// Updates an existing region
        /// </summary>
        /// <remarks>
        /// This method updates an existing region in the database based on the provided region ID and updated region information.
        /// </remarks>
        /// <param name="id">The ID of the region to be updated.</param>
        /// <param name="updatedRegion">The updated information of the region.</param>
        /// <returns>
        /// An IActionResult representing the outcome of the operation.
        /// </returns>
        [HttpPut("UpdateRegion/{id}")]
        public IActionResult Update(string id, Region_Base updatedRegion)
        {
            // Check if the provided id is valid
            if (!ObjectId.TryParse(id, out _))
            {
                return BadRequest("Invalid id format.");
            }

            // Find the region to be updated
            var existingRegion = _context._regions.Find(r => r._id == id).FirstOrDefault();
            if (existingRegion == null)
            {
                return NotFound("Region not found.");
            }

            // Update the fields of the existing region
            existingRegion.StateProvinceID = updatedRegion.StateProvinceID;
            existingRegion.StateProvinceCode = updatedRegion.StateProvinceCode;
            existingRegion.IsOnlyStateProvinceFlag = updatedRegion.IsOnlyStateProvinceFlag;
            existingRegion.StateProvinceName = updatedRegion.StateProvinceName;
            existingRegion.TerritoryID = updatedRegion.TerritoryID;
            existingRegion.CountryRegionCode = updatedRegion.CountryRegionCode;
            existingRegion.CountryRegionName = updatedRegion.CountryRegionName;

            // Replace the existing region with the updated one in the database
            var result = _context._regions.ReplaceOne(r => r._id == id, existingRegion);

            if (result.ModifiedCount > 0)
            {
                // Region successfully updated
                return Ok("Region successfully updated.");
            }
            else
            {
                // Region not found or not updated
                return NotFound("Region not found or not updated.");
            }
        }


        /// <summary>
        /// Deletes Region
        /// </summary>
        /// <remarks>
        /// This method deletes a region from the database based on the provided region ID.
        /// </remarks>
        /// <param name="id">The ID of the region to be deleted.</param>
        /// <returns>
        /// An IActionResult representing the outcome of the operation.
        /// </returns>
        [HttpDelete("DeleteRegion/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _context._regions.DeleteOne(x => x._id == id);

            if (result.DeletedCount > 0)
            {
                // Region successfully deleted
                return Ok("Region successfully deleted.");
            }
            else
            {
                // Region not found or not deleted
                return NotFound("Region not found or not deleted.");
            }
        }

    }
}
