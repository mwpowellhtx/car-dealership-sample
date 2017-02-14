using System.Threading.Tasks;

namespace Powell.Vehicles.Managers
{
    public interface IYearManager : IHibernateManagerBase
    {
        void Add();

        Task AddAsync();

        void Add(int year);

        Task AddAsync(int year);
    }
}
