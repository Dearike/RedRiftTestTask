using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;

namespace CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        CardStaticData ForCard(CardTypeId typeId);
    }
}