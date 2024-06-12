using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantShield.Data;
using PlantShield.Dtos;
using PlantShield.Models;
using System.Xml.Linq;

namespace PlantShield.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DiseaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("AddDisease")]
        public async Task<IActionResult> AddDisease([FromBody] DiseaseDto data)
        {
            if (data == null)
            {
                return BadRequest(new
                {
                    message = "Invalid Disease data (data is null)"
                });
            }
            var userData = _context.Users.Where(u => u.Email == data.Email).FirstOrDefault();

            var role = _context.Roles.Where(r => r.Name == "Admin").FirstOrDefault();
            


            var auth = _context.UserRoles.Where(u => u.UserId == userData.Id && u.RoleId == role.Id).FirstOrDefault();
            if (auth is null)
                return Unauthorized();

            var plantData =await _context.Plant.FindAsync(data.PlantId);

            if(plantData is null)
            {
                return BadRequest(new
                {
                    message = "Plant Id is Wrong"
                });
            }


            var mappedData = new Disease()
            {
                Name = data.Name,
                Solution = data.Solution,
                Other_Treatment = data.Other_Treatment,
                PlantId = data.PlantId
            };

            _context.Disease.Add(mappedData);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                message = "Disease Added successfully"
            });
        }
        [Authorize]
        [HttpGet("GetAllDisseasesAsync")]
        public async Task<IActionResult> GetAllDisseases()
        {
            var Disease = await _context.Disease.ToListAsync();

            if(Disease is null)
                return NotFound(new
                {
                    Message = "No Disease is found"
                });

            return Ok(Disease);
        }


        [Authorize]
        [HttpGet("GetDisseasesByName")]
        public async Task<IActionResult> GetDisseases(FindDiseaseDto dto)
        {
            var plant =await _context.Plant.Where(p => p.Name.ToLower() == dto.PlantName.ToLower()).FirstOrDefaultAsync();

            if (plant == null)
                return NotFound(new
                {
                    Message = "Unrecognized Plant Name"
                });

            var Disease=await _context.Disease.Where(d=> d.Name.ToLower() ==dto.DiseaseName.ToLower() && d.PlantId==plant.Id).FirstOrDefaultAsync();

            if (Disease == null)
                return NotFound(new
                {
                    Message = "Disease not found or entre all letters small"
                });

            return Ok(new
            {
                PlantName=plant.Name,
                DiseaseName = Disease.Name,
                Solution = Disease.Solution,
                Other_Treatment = Disease.Other_Treatment
            });
        }



        #region MyRegion
        //[Authorize]
        //[HttpGet("GetDisseasesById/{id}")]
        //public async Task<IActionResult> GetDisseasesById(int id)
        //{
        //    var Disaese = await _context.Disease.FindAsync(id);
        //    //var dis = await _context.dis.SingleOrDefaultAsync(m => m.Id == id);

        //    if (Disaese == null)
        //        return NotFound(new
        //        {
        //            Message = "Disease not found or entre all letters small"
        //        });

        //    return Ok(new
        //    {
        //        Name = Disaese.Name,
        //        Solution = Disaese.Solution,
        //        Other_Treatment=Disaese.Other_Treatment,
        //        PlantId=Disaese.PlantId
        //    });
        //}


        //[Authorize]
        //[HttpGet("GetDisseasesByName/{Name}")]
        //public async Task<IActionResult> GetDisseasesByName1(string name)
        //{
        //    var Disaese = await _context.Disease.Where(d=>d.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        //    //var dis = await _context.dis.SingleOrDefaultAsync(m => m.Id == id);

        //    if (Disaese == null)
        //        return NotFound(new
        //        {
        //            Message="Disease not found or entre all letters small"
        //        });

        //    return Ok(new
        //    {
        //        Name = Disaese.Name,
        //        Solution = Disaese.Solution,
        //        Other_Treatment = Disaese.Other_Treatment
        //    });
        //}

        #endregion

    }
}
