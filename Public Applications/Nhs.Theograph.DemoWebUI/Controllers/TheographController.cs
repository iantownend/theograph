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
    using Nhs.Theograph.DemoWebUI.Helpers;
    using Newtonsoft.Json;

    public class TheographController : Controller
    {
        private PatientEpisodeService patientEpisodeService;

        /// <summary>
        /// Initialises a new instance of the <see cref="TheographController"/> class.
        /// </summary>
        /// <param name="patientEpisodeService">The patient episode service.</param>
        public TheographController(PatientEpisodeService patientEpisodeService)
        {
            this.patientEpisodeService = patientEpisodeService;
        }

        [HttpGet]
        public ActionResult Index(string nhsNumber)
        {
            NhsNumber value = this.ValidateNhsNumber(nhsNumber);

            return this.View(this.patientEpisodeService.GetPatientEpisodesByNhsNumber(value));
        }

        [HttpGet]
        [ActionName("getData")]
        public ActionResult GetData(string nhsNumber)
        {
            NhsNumber value = this.ValidateNhsNumber(nhsNumber);

            var result = new JsonNetResult
            {
                Data = TheographHighchartAdaptors.GetPatientEpsiodesChart(
                    this.patientEpisodeService.GetPatientEpisodesByNhsNumber(value)),
#if DEBUG
                Formatting = Formatting.Indented
#else
                Formatting = Formatting.None
#endif
            };

            return result;
        }

        [HttpGet]
        [ActionName("getEpisodeData")]
        public ActionResult GetEpisodeData(string nhsNumber, string episodeId)
        {
            NhsNumber value = this.ValidateNhsNumber(nhsNumber);

            Guid validatorGuid;

            if (!Guid.TryParse(episodeId, out validatorGuid))
            {
                return new JsonNetResult();
            }

            var data = this.patientEpisodeService.GetPatientEpisodeEvents(value, new Core.Episode.EpisodeId { Value = episodeId });

            var result = new JsonNetResult
            {
                Data = TheographHighchartAdaptors.GetPatientEpsiodeEventsChart(data),
#if DEBUG
                Formatting = Formatting.Indented
#else
                Formatting = Formatting.None
#endif
            };

            return result;
        }

        /// <summary>
        /// Validates the specified NHS number value.
        /// </summary>
        /// <param name="nhsNumber">The NHS number.</param>
        /// <returns></returns>
        private NhsNumber ValidateNhsNumber(string nhsNumber)
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

            return value;
        }
    }
}