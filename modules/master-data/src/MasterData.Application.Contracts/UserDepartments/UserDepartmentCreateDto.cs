using System;
using System.Collections.Generic;
using System.Text;

namespace MasterData.UserDepartments
{
    public class UserDepartmentCreateDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string DepartmentCode { get; set; }
    }
}
