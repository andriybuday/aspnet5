using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IWorldRepository _repository;

        public AppController(IMailService service, IWorldRepository repository)
        {
            _mailService = service;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            var trips = _repository.GetAllTrips();

            return View(trips);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (string.IsNullOrWhiteSpace(email))
                {
                    ModelState.TryAddModelError("", "Could not send email, configuration problem");
                }

                var emailSent = _mailService.SendMail(email, email,
                    $"Contact Page from {model.Name} ({model.Email})", model.Message);

                if (emailSent)
                {
                    ModelState.Clear();

                    ViewBag.Message = "Mail Sent. Thanks!";
                }
            }

            return View();
        }
    }
}
