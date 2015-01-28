using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace LibSensu.NET.Models
{
    public class Alert
    {
        public string Name { get; set; }
        public string Output { get; set; }
        public string Source { get; set; }
        public int Status { get; set; }
    }
}
