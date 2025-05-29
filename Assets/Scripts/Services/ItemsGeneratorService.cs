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

            // Гарантируем, что общее количество кратно 3
            if (totalCount % 3 != 0)
            {
                totalCount -= totalCount % 3;
                Debug.LogWarning($"Adjusted total count to be divisible by 3: {totalCount}");
            }

            var result = new List<GameItemData>(totalCount);

            _figureTypes = Enum.GetValues(typeof(EFigureType)).Cast<EFigureType>().ToList();
            _animalTypes = Enum.GetValues(typeof(EAnimalType)).Cast<EAnimalType>().ToList();
            _colorTypes = Enum.GetValues(typeof(EColorType)).Cast<EColorType>().ToList();

            // Все возможные уникальные комбинации
            var allCombinations = new List<GameItemData>();

            foreach (var figure in _figureTypes)
            foreach (var animal in _animalTypes)
            foreach (var color in _colorTypes)
            {
                allCombinations.Add(new GameItemData(figure, animal, color));
            }

            // Перемешиваем комбинации для разнообразия
            Shuffle(allCombinations);

            int combinationIndex = 0;

            while (result.Count < totalCount && combinationIndex < allCombinations.Count)
            {
                var combo = allCombinations[combinationIndex++];
                int maxPossible = (totalCount - result.Count) / 3;
                int multiplier = _random.Next(1, Math.Min(4, maxPossible + 1)); // от 1 до 3 (т.е. 3, 6 или 9 штук)
                int countToAdd = multiplier * 3;

                for (int i = 0; i < countToAdd; i++)
                {
                    result.Add(new GameItemData(combo.FigureType, combo.AnimalType, combo.ColorType));
                }
            }

            // Если мы не добрали, то дополняем из уже добавленных
            while (result.Count < totalCount)
            {
                var existing = result[_random.Next(result.Count)];
                result.Add(new GameItemData(existing.FigureType, existing.AnimalType, existing.ColorType));
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