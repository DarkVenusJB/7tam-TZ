using System.Collections.Generic;
using Scripts.View;
using UnityEngine;
using Zenject;

namespace Scripts.Services
{
    public class GameStateService : IGameStateService
    {
        private List<GameUIItemView> _uiItems;
        private List<GameItemView> _gameItems;
        private EGameState _gameState;
       
        private readonly DiContainer _container;
        private readonly RectTransform _uiElementsHandler;
        private readonly GameUIItemView _circlePrefab;
        private readonly GameUIItemView _squarePrefab;
        private readonly GameUIItemView _trianglePrefab;
        private readonly GameUIItemView _capsulePrefab;

        public EGameState GameState => _gameState;
        public List<GameItemView> GameItems => _gameItems;
        
        public GameStateService(
            DiContainer container,
            RectTransform uiElementsHandler,
            GameUIItemView  circlePrefab,
            GameUIItemView squarePrefab,
            GameUIItemView trianglePrefab,
            GameUIItemView capsulePrefab)
        {
            _container = container;
            _uiElementsHandler = uiElementsHandler;
            _circlePrefab = circlePrefab;
            _squarePrefab = squarePrefab;
            _trianglePrefab = trianglePrefab;
            _capsulePrefab = capsulePrefab;
        }
        
        public void GameStarted()
        {
            _gameState = EGameState.Played;
        }

        public void AddNewElement()
        {
            
        }
    }
}