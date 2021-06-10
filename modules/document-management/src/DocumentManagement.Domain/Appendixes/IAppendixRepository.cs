using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace DocumentManagement.Appendixes
{
    public interface IAppendixRepository : IBasicRepository<Appendix, Guid>
    {
    }
}
