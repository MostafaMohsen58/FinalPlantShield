using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantShield.Data;
using PlantShield.Dtos;
using PlantShield.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PlantShield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("AddStation")]
        public async Task<IActionResult> CreateStation([FromBody] StationDto stationDto)
        {
            if (stationDto == null)
            {
                return BadRequest(new
                {
                    message = "Station data is null."
                });
            }
            Random random = new Random();

            var userData = _context.Users.Where(u => u.Email == stationDto.Email).FirstOrDefault();

            if (userData is null)
                return BadRequest(new
                {
                    message = "Email is not correct"
                });

            var station = new Station
            {
                Id = random.Next(10000, 99999).ToString(),
                Name = stationDto.Name,
                SoilType = stationDto.SoilType,
                PlantType = stationDto.PlantType,
                StartDate = stationDto.StartDate,
                EndDate = stationDto.EndDate,
                UserId = userData.Id
            };

            _context.Station.Add(station);

            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "Station Data Added successfully",
                StationId= station.Id
            });
        }

        [Authorize]
        [HttpGet("GetAllStation")]
        public async Task<IActionResult> GetAllStationAsync([FromQuery] EmailDto model)//for specific user 
        {
            var userData= _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (userData is null)
                return BadRequest(new
                {
                    message = "Email is Invalid"
                });

            var stations = await _context.Station.Where(s=>s.UserId==userData.Id).OrderBy(s=>s.CreatedAt).ToListAsync();

            List<ReturnStationDto> stationData = new List<ReturnStationDto>();

            foreach (var item in stations)
            {
                stationData.Add(new ReturnStationDto
                {
                    Id=item.Id,
                    Email=model.Email,
                    Name=item.Name,
                    SoilType=item.SoilType,
                    PlantType=item.PlantType,
                    StartDate=item.StartDate,
                    EndDate=item.EndDate
                });
            }
            return Ok(stationData);
        }

        [Authorize]
        [HttpDelete("DeleteStation")]
        public async Task<IActionResult> DeleteStation([FromBody] StationIdDto dto)
        {
            var station = await _context.Station.FindAsync(dto.StationId);
            if (station is null )
            {
                return BadRequest(new
                {
                    message = "Station doen't exist"
                });
            }

            var data = await _context.SensorData.Where(d=>d.StationId ==dto.StationId).FirstOrDefaultAsync();
            var irrigation = await _context.Irrigation.Where(d => d.StationId == dto.StationId).FirstOrDefaultAsync(); //delete stationId from related table

            if ( data is null || irrigation is null)
            {
                return BadRequest(new
                {
                    message = "Some thing is wrong"
                });
            }

            _context.Irrigation.Remove(irrigation);
            _context.SensorData.Remove(data);
            _context.Station.Remove(station);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Station Deleted successfully"
            });
        }

        [HttpGet("GetEmailByStationId")]
        public async Task<IActionResult> GetEmailByStationId([FromQuery] StationIdDto model)
        {

            if (model == null)
            {
                return BadRequest(new
                {
                    message = "model is not correct "
                });
            }

            var stationData = await _context.Station.FindAsync(model.StationId);

            if (stationData is null)
                return BadRequest(new
                {
                    Message = "Station Id doesn't exist"
                });

            var userData = await _context.Users.FindAsync(stationData.UserId);

            if (userData is null)
                return NotFound(new
                {
                    Message = "User doesn't exist "
                });

            return Ok(new
            {
                Email = $"{userData.Email}"
            });
        }

    }
}
