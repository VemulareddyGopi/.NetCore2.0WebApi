using System;
using System.Collections.Generic;
using System.Text;

namespace ASP.NetCore.Models.DTOS
{
    public class TokenDTO
    {
        public string token { get; set; }

        public string tokenType { get; set; } = "Bearer";
    }
}
