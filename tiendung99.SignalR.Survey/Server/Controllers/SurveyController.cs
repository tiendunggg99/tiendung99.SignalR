using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using tiendung99.SignalR.Survey.Server.Hubs;
using tiendung99.SignalR.Survey.Shared;

namespace tiendung99.SignalR.Survey.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly IHubContext<SurveyHub, ISurveyHub> hubContext;
        private static ConcurrentBag<Surveyy> surveys = new ConcurrentBag<Surveyy> {
          new Surveyy {
              Id = Guid.Parse("b00c58c0-df00-49ac-ae85-0a135f75e01b"),
              Title = "Are you excited about .NET 5.0?",
              ExpiresAt = DateTime.Now.AddMinutes(10),
              Options = new List<string>{ "Yes", "Nope", "meh", "PS5 just came out...", "None of the above, I got a series X" },
              Answers = new List<SurveyAnswer>{
                new SurveyAnswer { Option = "Yes" },
                new SurveyAnswer { Option = "Yes" },
                new SurveyAnswer { Option = "Yes" },
                new SurveyAnswer { Option = "Nope" },
                new SurveyAnswer { Option = "meh" },
                new SurveyAnswer { Option = "meh" },
                new SurveyAnswer { Option = "PS5 just came out..." },
                new SurveyAnswer { Option = "None of the above, I got a series X" }
              }
          },
          new Surveyy {
              Id = Guid.Parse("7e467e51-9999-427e-bf81-015076b9f24c"),
              Title = "What's the best food in the world?",
              ExpiresAt = DateTime.Now.AddMinutes(2),
              Options = new List<string>{ "Cheese", "Payoyo goat's cheese", "Tortilla", "Jamón", "Soylent!" },
              Answers = new List<SurveyAnswer>{
                new SurveyAnswer { Option = "Cheese" },
                new SurveyAnswer { Option = "Cheese" },
                new SurveyAnswer { Option = "Payoyo goat's cheese" },
                new SurveyAnswer { Option = "Payoyo goat's cheese" },
                new SurveyAnswer { Option = "Payoyo goat's cheese" },
                new SurveyAnswer { Option = "Payoyo goat's cheese" },
                new SurveyAnswer { Option = "Tortilla" },
                new SurveyAnswer { Option = "Jamón" },
                new SurveyAnswer { Option = "Jamón" },
                new SurveyAnswer { Option = "Jamón" }
              }
          },
        };

        public SurveyController(IHubContext<SurveyHub, ISurveyHub> surveyHub)
        {
            this.hubContext = surveyHub;
        }

        [HttpGet()]
        public IEnumerable<SurveySummary> GetSurveys()
        {
            return surveys.Select(s => s.ToSummary());
        }

        [HttpGet("{id}")]
        public ActionResult GetSurvey(Guid id)
        {
            var survey = surveys.SingleOrDefault(t => t.Id == id);
            if (survey == null) return NotFound();

            return new JsonResult(survey);
        }

        // Note an [ApiController] will automatically return a 400 response if any
        // of the data annotation valiadations defined in AddSurveyModel fails
        [HttpPut()]
        public async Task<Surveyy> AddSurvey([FromBody] AddSurveyModel addSurveyModel)
        {
            var survey = new Surveyy
            {
                Title = addSurveyModel.Title,
                ExpiresAt = DateTime.Now.AddMinutes(addSurveyModel.Minutes.Value),
                Options = addSurveyModel.Options.Select(o => o.OptionValue).ToList()
            };

            surveys.Add(survey);
            await this.hubContext.Clients.All.SurveyAdded(survey.ToSummary());
            return survey;
        }

        [HttpPost("{surveyId}/answer")]
        public async Task<ActionResult> AnswerSurvey(Guid surveyId, [FromBody] SurveyAnswer answer)
        {
            var survey = surveys.SingleOrDefault(t => t.Id == surveyId);
            if (survey == null) return NotFound();
            if (((IExpirable)survey).IsExpired) return StatusCode(400, "This survey has expired");

            // warning, this isnt thread safe since we store answers in a List
            survey.Answers.Add(new SurveyAnswer
            {
                SurveyId = surveyId,
                Option = answer.Option
            });

            // Notify anyone connected to the survey group
            // ofc sending the entire survey all the time is inefficient, but enough in this tutorial
            await this.hubContext.Clients.Group(surveyId.ToString()).SurveyUpdated(survey);

            return new JsonResult(survey);
        }
    }
}
