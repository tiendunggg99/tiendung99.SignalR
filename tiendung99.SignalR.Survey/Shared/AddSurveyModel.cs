using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tiendung99.SignalR.Survey.Shared
{
    public class AddSurveyModel
    {
        public int? Minutes { get; set; }
        public List<OptionCreateModel> Options { get; init; } = new List<OptionCreateModel>();

        public void RemoveOption(OptionCreateModel option) => this.Options.Remove(option);
        public void AddOption() => this.Options.Add(new OptionCreateModel());
    }
    public class OptionCreateModel
    {
        public string OptionValue { get; set; }
    }
}
