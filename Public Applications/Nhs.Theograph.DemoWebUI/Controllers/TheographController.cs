

namespace Nhs.Theograph.DemoWebUI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Nhs.Theograph.Core;
    using Nhs.Theograph.Core.Services;

    public class TheographController : Controller
    {
        private PatientEpisodeService patientEpisodeService;

        public TheographController(PatientEpisodeService patientEpisodeService)
        {
            this.patientEpisodeService = patientEpisodeService;
        }

        [HttpGet]
        public ActionResult Index(string nhsNumber)
        {
            NhsNumber value = null;

            try
            {
                value = new NhsNumber(nhsNumber);
            }
            catch (ArgumentException)
            {
                this.RedirectToAction("Index", "Home");
            }

            return this.View(this.patientEpisodeService.GetPatientEpisodesByNhsNumber(value));
        }
    }
}