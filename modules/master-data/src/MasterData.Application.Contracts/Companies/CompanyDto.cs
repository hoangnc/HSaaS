using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.Companies
{
    public class CompanyDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}
