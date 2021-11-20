using People.Domain.Entities;
using People.Domain.Interfaces.Repositories;
using People.Domain.Interfaces.Services;
using System.Data;

namespace People.Services.Repositories
{
    public class TechnologyRepository : BaseRepository<string, TechnologyEntity>, ITechnologyRepository
    {
        public TechnologyRepository(IDbConnection dbConnection, ILogService logService) : base(dbConnection, logService)
        {
        }
    }
}
