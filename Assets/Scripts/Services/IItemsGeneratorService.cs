using System.Collections.Generic;
using Scripts.Data;

namespace Scripts.Services
{
    public interface IItemsGeneratorService
    {
        bool IsAllItemsReady { get; }
        List<GameItemData> GenerateItems(GeneratorData generatorData);
    }
}