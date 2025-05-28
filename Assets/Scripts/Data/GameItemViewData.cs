using System;
using System.Collections.Generic;
using Providers;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(fileName = "GameItemViewData", menuName = "Data/ItemViewData")]
    public class GameItemViewData : ScriptableObject
    {
        [SerializeField] private List<FigureVisualVariant> _figureVariant;
        [SerializeField] private List<AnimalVisualVariant> _animalVariant;
        [SerializeField] private List<ColorVisualVariant> _colorVariant;
        
        public IReadOnlyList<FigureVisualVariant> FigureVariant => _figureVariant;
        public IReadOnlyList<AnimalVisualVariant> AnimalVariant => _animalVariant;
        public IReadOnlyList<ColorVisualVariant> ColorVariant => _colorVariant;
    }
    
    [Serializable]
    public class FigureVisualVariant
    {
        public EFigureType Figure;
        public Sprite Sprite;
    }
    
    [Serializable]
    public class AnimalVisualVariant
    {
        public EAnimalType Animal;
        public Sprite Sprite;
    }

    [Serializable]
    public class ColorVisualVariant
    {
        public EColorType ColorName;
        public Color Color;
    }
    
}