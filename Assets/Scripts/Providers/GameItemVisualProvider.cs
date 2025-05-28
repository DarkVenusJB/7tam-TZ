using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;

namespace Providers
{
    public class GameItemVisualProvider : IGameItemVisualProvider
    {
        private readonly Dictionary<EFigureType, Sprite> _figureSprites;
        private readonly Dictionary<EAnimalType, Sprite> _animalSprites;
        private readonly Dictionary<EColorType, Color> _colorSprites;

        public GameItemVisualProvider(GameItemViewData viewData)
        {
            _figureSprites = new Dictionary<EFigureType, Sprite>();
            foreach (var variant in viewData.FigureVariant)
            {
                if (_figureSprites.ContainsKey(variant.Figure))
                {
                    Debug.LogWarning($"Duplicate figure key: {variant.Figure}");
                    continue;
                }
                _figureSprites.Add(variant.Figure, variant.Sprite);
            }

            _animalSprites = new Dictionary<EAnimalType, Sprite>();
            foreach (var variant in viewData.AnimalVariant)
            {
                if (_animalSprites.ContainsKey(variant.Animal))
                {
                    Debug.LogWarning($"Duplicate animal key: {variant.Animal}");
                    continue;
                }
                _animalSprites.Add(variant.Animal, variant.Sprite);
            }

            _colorSprites = new Dictionary<EColorType, Color>();
            foreach (var variant in viewData.ColorVariant)
            {
                if (_colorSprites.ContainsKey(variant.ColorName))
                {
                    Debug.LogWarning($"Duplicate color key: {variant.ColorName}");
                    continue;
                }
                _colorSprites.Add(variant.ColorName, variant.Color);
            }
        }

        public Sprite GetFigureSprite(EFigureType figureType)
        {
            return _figureSprites.TryGetValue(figureType, out var sprite) ? sprite : null;
        }

        public Sprite GetAnimalSprite(EAnimalType animalType)
        {
            return _animalSprites.TryGetValue(animalType, out var sprite) ? sprite : null;
        }

        public Color GetColor(EColorType colorType)
        {
            return _colorSprites.TryGetValue(colorType, out var color) ? color : Color.white;
        }
    }
}