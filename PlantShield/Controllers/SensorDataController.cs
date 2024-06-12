using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PlantShield.Data;
using PlantShield.Dtos;
using PlantShield.Models;

namespace PlantShield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorDataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SensorDataController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost("SetUserDataByEmail")]
        public async Task<IActionResult> SetOrUpdateUserDataByEmail([FromBody] SensorDataDto data)
        {
            if (data == null)
            {
                return BadRequest(new
                {
                    message = "Invalid sensor data (data is null)"
                });
            }

            var userData = await _context.Users.Where(u => u.Email == data.Email).FirstOrDefaultAsync();

            if (userData is null)
                return BadRequest(new
                {
                    message = "Email doesn't exist"
                });

            var DataExistOrNot = _context.SensorData.Where(s => s.StationId == data.StationId).FirstOrDefault();

            if (DataExistOrNot is not null)
            {
                DataExistOrNot.TemperatureInCelicus = data.TemperatureInCelicus;
                DataExistOrNot.TemperatureInFerhnient = data.TemperatureInFerhnient;
                DataExistOrNot.Moisture = data.Moisture;
                DataExistOrNot.Humdity = data.Humdity;

                _context.SaveChanges();

                return Ok(new
                {
                    message = "Sensor Data is Updated"
                });
            }

            else
            {
                var seedData = new SensorData
                {
                    UserId = userData.Id,
                    TemperatureInCelicus = data.TemperatureInCelicus,
                    TemperatureInFerhnient = data.TemperatureInFerhnient,
                    Humdity = data.Humdity,
                    Moisture = data.Moisture,
                    StationId = data.StationId
                };

                _context.SensorData.Add(seedData);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Set Sensor Data successfully"
                });
            }

        }

        [Authorize]
        [HttpGet("GetSensorDataByEmail")]
        public async Task<IActionResult> getSensorData([FromQuery] EmailDto model)
        {
            var Id = await _context.Users.Where(u => u.Email == model.Email).Select(u => u.Id).FirstOrDefaultAsync();

            if (Id is null)
                return BadRequest(new
                {
                    Message = "User doesn't Exist"
                });

            var data = await _context.SensorData.Where(s => s.UserId == Id).FirstOrDefaultAsync();

            if (data is null)
                return Ok(new
                {
                    Message = "No Data for this user"
                });

            return Ok(new { TemperatureInCelicus = data.TemperatureInCelicus, TemperatureInFerhnient = data.TemperatureInFerhnient,
                Moisture = data.Moisture, Humdity = data.Humdity, StationId = data.StationId });
        }


        [Authorize]
        [HttpGet("GetSensorDataByStationId")]
        public async Task<IActionResult> GetSensorDataByStationId([FromQuery] StationIdDto model)
        {
            var sensorData=await _context.SensorData.Where(d=>d.StationId == model.StationId).FirstOrDefaultAsync();

            if(sensorData is null)
                return BadRequest(new
                {
                    Message="Station Id doesn't exixt or no data for this station"
                });

            return Ok(new 
            { 
                TemperatureInCelicus = sensorData.TemperatureInCelicus,
                TemperatureInFerhnient =sensorData.TemperatureInFerhnient, 
                Humdity =sensorData.Humdity,
                Moisture =sensorData.Moisture
            });
        }

    }
}
