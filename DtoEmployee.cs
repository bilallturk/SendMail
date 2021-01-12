using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail
{
    public class DtoEmployee
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime JobStartDate { get; set; }
    }
}
