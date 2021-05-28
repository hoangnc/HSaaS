using Volo.Abp.Application.Dtos;

namespace MasterData.Modules
{
    public class GetModulesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
