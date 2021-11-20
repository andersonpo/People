using People.Domain.Entities;

namespace People.Domain.Interfaces.Repositories
{
    public interface ITechnologyRepository : IBaseRepository<string, TechnologyEntity>
    {
        public const string TecnologyTable = "TECHNOLOGY";
    }
}
