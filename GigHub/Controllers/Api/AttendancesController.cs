using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            if (_context.Attendances.Any(a => a.AttendeeID == userId && a.GigID == dto.GigId))
                return BadRequest("The attendance already exists.");
            var attendance = new Attendance
            {
                GigID = dto.GigId,
                AttendeeID = userId,
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _context.Attendances
                .SingleOrDefault(a => a.AttendeeID == userId
                && a.GigID == id);

            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);
            _context.SaveChanges();

            return Ok(id);
        }

    }
}
