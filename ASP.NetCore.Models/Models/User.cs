using System;
using System.Collections.Generic;
using System.Text;

namespace ASP.NetCore.Models.Models
{
   public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public DateTime? Dob { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
