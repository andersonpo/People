using People.Domain.Interfaces.Services;
using People.Domain.Interfaces.Repositories;
using People.Domain.DTOs.Requests;
using People.Domain.Entities;
using People.Domain.Interfaces;

namespace People.Services.Services
{
    public class TechnologyService : BaseService<string, TechnologyRequestDTO, TechnologyEntity>, ITechnologyService
    {
        public TechnologyService(ITechnologyRepository technologyRepository, ILogService logService, IApiNotification apiNotification) : base(technologyRepository, logService, apiNotification)
        {
        }
    }
}
