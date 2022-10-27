using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreManage.Entity;

namespace BookStoreManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly BookManageContext _context;

        public TestController(BookManageContext context)
        {
            _context = context;
        }

        // GET: api/Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Field>>> GetFields()
        {
            return await _context.Fields.ToListAsync();
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Field>> GetField(int id)
        {
            var @field = await _context.Fields.FindAsync(id);

            if (@field == null)
            {
                return NotFound();
            }

            return @field;
        }

        // PUT: api/Test/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutField(int id, Field @field)
        {
            if (id != @field.FieldID)
            {
                return BadRequest();
            }

            _context.Entry(@field).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FieldExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Test
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Field>> PostField(Field @field)
        {
            _context.Fields.Add(@field);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetField", new { id = @field.FieldID }, @field);
        }

        // DELETE: api/Test/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(int id)
        {
            var @field = await _context.Fields.FindAsync(id);
            if (@field == null)
            {
                return NotFound();
            }

            _context.Fields.Remove(@field);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FieldExists(int id)
        {
            return _context.Fields.Any(e => e.FieldID == id);
        }
    }

    // public class ApartBuildingController : ControllerBase
    // {
    //     private readonly IConfiguration _config;

    //     public ApartBuildingController(IConfiguration config)
    //     {
    //         _config = config;
    //     }

    //     [HttpGet]
    //     public async Task<ActionResult<List<Building>>> GetAll()
    //     {
    //         var buildingDict = new Dictionary<int, Building>();
    //         using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    //         var apartBuilding = await connection.QueryAsync<Building, Apartment, Building>("SELECT * FROM Buildings b JOIN Apartments a ON a.BuildingId = b.BuildingId", (building, apartment) =>
    //         {
    //             if (!buildingDict.TryGetValue(building.BuildingId, out var currentBuilding))
    //             {
    //                 buildingDict.Add(building.BuildingId, currentBuilding = building);
    //                 currentBuilding.Apartments.Add(apartment);
    //             }
    //             return currentBuilding;
    //         });
    //         return Ok(buildingDict.Values.ToArray());
    //     }
    // }

    // public class OwnerController : ControllerBase
    // {

    //     private readonly IConfiguration _config;

    //     public OwnerController(IConfiguration config)
    //     {
    //         _config = config;
    //     }

    //     [HttpGet]
    //     public async Task<ActionResult<List<Owner>>> GetAll()
    //     {
    //         using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    //         var owners = await connection.QueryAsync<Owner>("select * from Owners");
    //         return Ok(owners);
    //     }

    //     [HttpGet("{id}")]
    //     public async Task<ActionResult<Owner>> Get(int id)
    //     {
    //         using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    //         var owner = await connection.QueryFirstAsync<Owner>("select * from Owners where OwnerId = @OwnerId", new { OwnerId = id });
    //         return Ok(owner);
    //     }

    //     [HttpPost]
    //     public async Task<ActionResult<List<Owner>>> Post(Owner owner)
    //     {
    //         using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    //         await connection.ExecuteAsync("insert into Owners ([Name], [Phone], [Email], [OwnerImgUrl]) values (@Name, @Phone, @Email, @OwnerImgUrl)", owner);
    //         return Ok(await SelectAll(connection));
    //     }

    //     [HttpPut]
    //     public async Task<ActionResult<List<Owner>>> Put(Owner owner)
    //     {
    //         using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    //         await connection.ExecuteAsync("update Owners set Name = @Name, Phone = @Phone, Email = @Email, OwnerImgUrl = @OwnerImgUrl where OwnerId = @OwnerId", owner);
    //         return Ok(await SelectAll(connection));
    //     }

    //     [HttpDelete("{id}")]
    //     public async Task<ActionResult<List<Owner>>> Delete(int id)
    //     {
    //         using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    //         await connection.ExecuteAsync("delete from Owners where OwnerId = @OwnerId", new { OwnerId = id });
    //         return Ok(await SelectAll(connection));
    //     }

    //     private static async Task<IEnumerable<Owner>> SelectAll(SqlConnection connection)
    //     {
    //         return await connection.QueryAsync<Owner>("select * from Owners");
    //     }
    // }
}
