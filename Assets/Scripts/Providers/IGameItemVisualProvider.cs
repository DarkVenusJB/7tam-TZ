using Scripts.Data;
using UnityEngine;

namespace Providers
{
    public interface IGameItemVisualProvider
    {
        Sprite GetFigureSprite(EFigureType figureType);
        Sprite GetAnimalSprite(EAnimalType animalType);
        Color GetColor(EColorType colorType);
    }
}