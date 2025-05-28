using System;
using Providers;
using Scripts.Data;
using Scripts.View;
using Zenject;

namespace Scripts.Services
{
    public class GameItemFactory : IGameItemFactory 
    {
        private readonly DiContainer _container;

        private readonly GameItemView _circlePrefab;
        private readonly GameItemView _squarePrefab;
        private readonly GameItemView _trianglePrefab;
        private readonly GameItemView _capsulePrefab;

        private readonly GameItemVisualProvider _visualProvider;

        public GameItemFactory(
            DiContainer container,
            GameItemView circlePrefab,
            GameItemView squarePrefab,
            GameItemView trianglePrefab,
            GameItemView capsulePrefab)
        {
            _container = container;
            _circlePrefab = circlePrefab;
            _squarePrefab = squarePrefab;
            _trianglePrefab = trianglePrefab;
            _capsulePrefab = capsulePrefab;
        }

        public GameItemView Create(GameItemData data)
        {
            var prefab = data.FigureType switch
            {
                EFigureType.Circle => _circlePrefab,
                EFigureType.Square => _squarePrefab,
                EFigureType.Triangle => _trianglePrefab,
                EFigureType.Capsule => _capsulePrefab,
                _ => throw new ArgumentOutOfRangeException()
            };

            var view = _container.InstantiatePrefabForComponent<GameItemView>(prefab);

            view.Init(data);

            return view;
        }
    }
}