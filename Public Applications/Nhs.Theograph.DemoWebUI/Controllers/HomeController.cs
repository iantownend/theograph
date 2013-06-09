namespace Nhs.Theograph.DemoWebUI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Nhs.Theograph.Core;
    using Nhs.Theograph.Core.Services;
    using Nhs.Theograph.DemoWebUI.Models;

    public class HomeController : Controller
    {
        private PatientEpisodeService patientEpisodeService;

        public HomeController(PatientEpisodeService patientEpisodeService)
        {
            this.patientEpisodeService = patientEpisodeService;
        }

        public ActionResult Index()
        {
            IList<PatientViewModel> viewModel = new List<PatientViewModel>();

            foreach (var patient in this.patientEpisodeService.GetPatients())
            { 
                viewModel.Add(new PatientViewModel(patient));
            }

            return this.View(viewModel);
        }
    }
}
