namespace CinemaSystem.Web.Controllers
{
    using CinemaSystem.Services.Data.Interfaces;
    using CinemaSystem.Web.ViewModels.Cinema;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using static CinemaSystem.Common.GeneralApplicationConstants;
    using static CinemaSystem.Common.NotificationMessagesConstants;


    [Authorize]
    public class CinemaController : Controller
    {
        private readonly ICinemaService cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                IEnumerable<CinemaViewModel> cinemas = await cinemaService.GetAllCinemasAsync();
                return View(cinemas);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = GeneralError;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
