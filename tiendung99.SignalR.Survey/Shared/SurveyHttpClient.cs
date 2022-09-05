using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace tiendung99.SignalR.Survey.Shared
{
    public class SurveyHttpClient
    {
        private readonly HttpClient http;

        public SurveyHttpClient(HttpClient http)
        {
            this.http = http;
        }

        public async Task<SurveySummary[]> GetSurveys()
        {
            return await this.http.GetFromJsonAsync<SurveySummary[]>("api/survey");
        }

        public async Task<Surveyy> GetSurvey(Guid surveyId)
        {
            return await this.http.GetFromJsonAsync<Surveyy>($"api/survey/{surveyId}");
        }

        public async Task<HttpResponseMessage> AddSurvey(AddSurveyModel survey)
        {
            return await this.http.PutAsJsonAsync<AddSurveyModel>("api/survey", survey);
        }

        public async Task<HttpResponseMessage> AnswerSurvey(Guid surveyId, SurveyAnswer answer)
        {
            return await this.http.PostAsJsonAsync<SurveyAnswer>($"api/survey/{surveyId}/answer", answer);
        }
    }
}
