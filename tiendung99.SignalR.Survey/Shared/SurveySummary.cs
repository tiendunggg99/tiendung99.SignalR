using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tiendung99.SignalR.Survey.Shared
{
    public record SurveySummary
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public DateTime ExpiresAt { get; init; }
        public List<string> Options { get; init; }
        public SurveySummary ToSummary() => new SurveySummary
        {
            Id = this.Id,
            Title = this.Title,
            Options = this.Options,
            ExpiresAt = this.ExpiresAt
        };
    }
}
