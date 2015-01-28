using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSensu.NET.Models
{
    public class Ping
    {
        public string Status { get; set; }
        public long ResponseTime { get; set; }
    }
}
