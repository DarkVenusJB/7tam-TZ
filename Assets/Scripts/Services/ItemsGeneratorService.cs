using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Data;
using UnityEngine;
using Random = System.Random;

namespace Scripts.Services
{
    public class ItemsGeneratorService : IItemsGeneratorService
    {
        private List<EFigureType> _figureTypes = new();
        private List<EAnimalType> _animalTypes = new();
        private List<EColorType> _colorTypes = new();
        private bool _isAllItemsReady;
        
        private readonly Random _random = new();
        
        public bool IsAllItemsReady => _isAllItemsReady;
        
        public List<GameItemData> GenerateItems(GeneratorData generatorData)
        {
            var totalCount = _random.Next(generatorData.MinMaxItemsCount.x, generatorData.MinMaxItemsCount.y);
            
            _isAllItemsReady = false;
            
            if (totalCount % 3 != 0)
            {
                Debug.LogWarning("Total count must be divisible by 3.");
                totalCount -= totalCount % 3;
            }

            var result = new List<GameItemData>(totalCount);

            _figureTypes = Enum.GetValues(typeof(EFigureType)).Cast<EFigureType>().ToList();
            _animalTypes = Enum.GetValues(typeof(EAnimalType)).Cast<EAnimalType>().ToList();
            _colorTypes = Enum.GetValues(typeof(EColorType)).Cast<EColorType>().ToList();

            while (result.Count < totalCount)
            {
                var figure = _figureTypes[_random.Next(_figureTypes.Count)];
                var animal = _animalTypes[_random.Next(_animalTypes.Count)];
                var color = _colorTypes[_random.Next(_colorTypes.Count)];
                var countToAdd = Math.Min(3 * _random.Next(1, 4), totalCount - result.Count);

                for (int i = 0; i < countToAdd; i++)
                {
                    result.Add(new GameItemData(figure, animal, color));
                }
            }

            Shuffle(result);

            _isAllItemsReady = true;
            
            return result;
        }

        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}