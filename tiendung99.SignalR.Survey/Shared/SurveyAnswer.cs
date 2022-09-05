using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tiendung99.SignalR.Survey.Shared
{
    public record SurveyAnswer
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid SurveyId { get; init; }
        public string Option { get; init; }
    }
}
