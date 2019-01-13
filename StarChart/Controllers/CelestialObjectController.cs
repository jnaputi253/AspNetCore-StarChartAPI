using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            if (celestialObject == null)
            {
                return NotFound();
            }

            celestialObject.Satellites =
                _context.CelestialObjects.Where(satellite => satellite.OrbitedObjectId == id).ToList();

            return Ok(celestialObject);
        }

        [HttpGet("{name}", Name = "GetByName")]
        public IActionResult GetByName(string name)
        {
            var storedCelestialObject = _context.CelestialObjects.Find(name);
            if (storedCelestialObject == null)
            {
                return NotFound();
            }

            storedCelestialObject.Satellites = _context.CelestialObjects
                .Where(celestialObject => celestialObject.OrbitedObjectId == storedCelestialObject.Id)
                .ToList();

            return Ok(storedCelestialObject);
        }
    }
}
