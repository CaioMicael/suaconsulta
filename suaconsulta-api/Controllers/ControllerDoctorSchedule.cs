using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaconsulta_api.Data;

namespace suaconsulta_api.Controllers
{
    [Route("api/DoctorSchedule")]
    [ApiController]
    public class ControllerDoctorSchedule : ControllerBase
    {
        [HttpGet]
        [Route("ListDoctorSchedule/")]
        public async Task<IActionResult> GetListScheduleDoctor([FromServices] AppDbContext _context, [FromRoute] int DoctorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Response = await _context.DoctorSchedule
                .Include(D => D.Doctor)
                .OrderBy(D => D.Id)
                .Where(D => D.DoctorId == DoctorId)
                .ToListAsync();

            if (Response == null)
            {
                return NotFound();
            }
            return Ok(Response);
        }
    }
}
