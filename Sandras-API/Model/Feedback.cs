using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sandras_API.Model
{
    public class Feedback
    {

        public int FeedbackTypeId { get; set; }

        public string Nombre { get; set; }

        public string Email { get; set; }

        public string Nota { get; set; }
    }
}
