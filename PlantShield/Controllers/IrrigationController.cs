using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantShield.Data;
using PlantShield.Dtos;
using PlantShield.Models;

namespace PlantShield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IrrigationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IrrigationController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpPost("SetModeByEmail")]
        public async Task<IActionResult> SetMode(IrrigationDto data)
        {
            if (data == null)
            {
                return BadRequest(new
                {
                    message = "Invalid data"
                });
            }

            var Id = _context.Users.Where(e => e.Email == data.Email).Select(e => e.Id).FirstOrDefault();

            if(Id is null)
                return BadRequest(new
                {
                    message = "Some Error is happen"
                });

            var seedData = new Irrigation
            {
                UserId = Id,
                IsAutomatic = data.IsAutomatic,
                PumpState = data.PumpState,
                StationId = data.StationId
            };

            _context.Irrigation.Add(seedData);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Set Mode done successfully"
            });
        }

        //[Authorize]


        [HttpGet("GetModeByEmail")]
        public async Task<IActionResult> GetMode([FromQuery] GetStationDto model)
        {

            var Id = await _context.Users.Where(u => u.Email == model.Email).Select(u => u.Id).FirstOrDefaultAsync();

            if (Id is null)
                return BadRequest(new
                {
                    message = "Email is wrong Or doesn't exist"
                });

            var data = await _context.Irrigation.Where(i => i.UserId == Id && i.StationId == model.StationId).FirstOrDefaultAsync();

            if (data is null)
                return BadRequest(new
                {
                    message = "No Data for this Email or Station Id is Wrong"
                });

            return Ok(new { IsAutomatic = data.IsAutomatic, PumpState = data.PumpState, StationId = data.StationId });

            #region Old Logic
            //var data =await _context.Irrigation.Where(s => s.UserId == Id).ToListAsync();

            //if (data.Count()==0)
            //    return BadRequest("no data for this user");

            //List<IrrigationDto> irrigationData=new List<IrrigationDto>();

            //foreach (var item in data)
            //{
            //    irrigationData.Add(new IrrigationDto{
            //        State=item.State,
            //        IsAutomaticOrMannual=item.IsAutomaticOrMannual,
            //        Email=model.Email
            //    });
            //}
            //return Ok(irrigationData); 
            #endregion
        }
    }
}
