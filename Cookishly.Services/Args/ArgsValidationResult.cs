using System.Collections.Generic;
using System.Linq;

namespace Cookishly.Services.Args
{
    public class ArgsValidationResult
    {
        public IList<string> Errors { get; set; }
        public bool HasErrors { get { return Errors.Any(); } }

        public ArgsValidationResult()
        {
            Errors = new List<string>();
        }
    }
}