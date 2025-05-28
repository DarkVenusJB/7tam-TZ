using Scripts.Data;
using Scripts.View;

namespace Scripts.Services
{
    public interface IGameItemFactory
    {
        GameItemView Create(GameItemData data);
    }
}