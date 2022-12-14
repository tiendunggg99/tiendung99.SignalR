using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tiendung99.SignalR.Survey.Shared
{
    public record Surveyy : IExpirable
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Title { get; init; }
        public DateTime ExpiresAt { get; init; }
        public List<string> Options { get; init; } = new List<string>();
        public List<SurveyAnswer> Answers { get; init; } = new List<SurveyAnswer>();

        public SurveySummary ToSummary() => new SurveySummary
        {
            Id = this.Id,
            Title = this.Title,
            Options = this.Options,
            ExpiresAt = this.ExpiresAt
        };
    }

    public record SurveyAnswer
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid SurveyId { get; init; }
        public string Option { get; init; }
    }
}
