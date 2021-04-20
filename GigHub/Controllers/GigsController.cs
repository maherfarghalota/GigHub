using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult MyFollowings()
        {
            var userId = User.Identity.GetUserId();
            var following = _context.Followings
                .Where(f => f.FollowerID == userId)
                .ToList();
            return View(following);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs
                .Where(g => g.ArtistID == userId &&
                g.dateTime > DateTime.Now &&
                !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeID == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();

            var attendances = _context.Attendances
                .Where(a => a.AttendeeID == userId
                && a.Gig.dateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.GigID);

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = attendances
            };
            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Heading = "Add a Gig",
                Genres = _context.Genres.ToList()
            };
            return View("GigForm", viewModel);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Edit(int ID)
        {
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Single(g => g.ID == ID && g.ArtistID == userId);
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList(),
                Id = gig.ID,
                Date = gig.dateTime.ToString("d MMM yyyy"),
                Time = gig.dateTime.ToString("HH:mm"),
                Genre = gig.GenreID,
                Venue = gig.Venue,
                Heading = "Edit a Gig"
            };
            return View("GigForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            var gig = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.ID == id);
            if (gig == null)
                return HttpNotFound();
            var viewModel = new GigDetailsViewModel { Gig = gig };
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances
                    .Any(a => a.GigID == gig.ID && a.AttendeeID == userId);

                viewModel.IsFollowing = _context.Followings
                    .Any(f => f.FolloweeID == gig.ArtistID && f.FollowerID == userId);
            }
            return View("Details", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
            var gig = new Gig
            {
                ArtistID = User.Identity.GetUserId(),
                dateTime = viewModel.GetDateTime(),
                GenreID = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("GigForm", viewModel);
            }
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.ID == viewModel.Id && g.ArtistID == userId);

            gig.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);


            _context.SaveChanges();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}