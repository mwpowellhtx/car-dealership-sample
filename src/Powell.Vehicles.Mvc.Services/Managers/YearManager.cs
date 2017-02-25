using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Powell.Vehicles.Managers
{
    using Data;
    using Vehicles;
    using static DateTime;

    public class YearManager : ControllerManager, IYearManager
    {
        public YearManager(IHibernateRepository repository)
            : base(repository)
        {
        }

        private static IEnumerable<Year> CreateNewYears(Year maxYear)
        {
            var next = UtcNow;

            var delta = next.Year - maxYear.Value.Year;

            while (delta > 0)
            {
                yield return new Year {Value = Parse($"1/1/{maxYear.Value.Year + delta--}")};
            }
        }

        public virtual void Add()
        {
            // TODO: TBD: MaxOrDefault would be interesting here...
            var maxYear = GetAllAsync<Year>().Result.OrderBy(x => x.Value).LastOrDefault();
            SaveOrUpdateAsync(CreateNewYears(maxYear).OrderBy(x => x.Value).ToArray()).Wait();
        }

        public virtual Task AddAsync()
        {
            return Task.Run(() => Add());
        }

        public virtual void Add(int year)
        {
            if (GetAllAsync<Year>(x => x.Value.Year == year).Result.Any()) return;
            SaveOrUpdate(new Year {Value = Parse($"1/1/{year}")});
        }

        public virtual Task AddAsync(int year)
        {
            return Task.Run(() => Add(year));
        }
    }
}
