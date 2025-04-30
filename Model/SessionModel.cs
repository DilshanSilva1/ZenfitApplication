using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draft2.Model
{
    public class SessionModel
    {
        public int SessionId { get; set; }
        public int CustomerId { get; set; }
        public double WeightKg { get; set; }
        public DateTime DateOfSession { get; set; }
    }
}
