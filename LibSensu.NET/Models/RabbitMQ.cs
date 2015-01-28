using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSensu.NET.Models
{
    class RabbitMQ
    {
        public string hostname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string vhost { get; set; }
        public string queue { get; set; }
    }
}
