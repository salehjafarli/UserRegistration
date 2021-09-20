using Serilog.Events;
using Serilog.Formatting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UserRegistrationWebApi.Helpers
{
    public class MyTextFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            output.Write($"[{logEvent.Timestamp}]| Level - {logEvent.Level}| " +
                $"Message - {logEvent.MessageTemplate.Text}");
        }
    }
}
