using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.dateTime > DateTime.Now &&
                !g.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query)
                    || g.Genre.name.Contains(query)
                    || g.Venue.Contains(query));
            }

            var userId = User.Identity.GetUserId();
            var attendances = _context.Attendances
                .Where(a => a.AttendeeID == userId
                && a.Gig.dateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigID);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };
            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}