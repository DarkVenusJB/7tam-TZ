using System;
using System.Collections.Generic;
using DG.Tweening;
using Scripts.Data;
using Scripts.View;
using UnityEngine;
using Zenject;

namespace Scripts.Services
{
    public class GameStateService : IGameStateService
    {
        private List<GameUIItemView> _uiItems = new();
        private List<GameItemView> _gameItems = new();
        private EGameState _gameState;
       
        private readonly DiContainer _container;
        private readonly RectTransform _uiElementsHandler;
        private readonly GameUIItemView _circlePrefab;
        private readonly GameUIItemView _squarePrefab;
        private readonly GameUIItemView _trianglePrefab;
        private readonly GameUIItemView _capsulePrefab;

        private const int GAME_OVER_UI_ELEMENTS_COUNT = 7;
        
        public EGameState GameState => _gameState;
        public List<GameItemView> GameItems => _gameItems;

        public event Action OnGameCompleted;
        public event Action OnGameOver;
        
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
        
        public void GameStarted(List<GameItemView> gameItems)
        {
            _gameItems = gameItems;
            
            _gameState = EGameState.Played;
        }

        public void AddNewElement(GameItemView gameItemView)
        {
            GameItems.Remove(gameItemView);
            
            var prefab = gameItemView.Data.FigureType switch
            {
                EFigureType.Circle => _circlePrefab,
                EFigureType.Square => _squarePrefab,
                EFigureType.Triangle => _trianglePrefab,
                EFigureType.Capsule => _capsulePrefab,
                _ => throw new ArgumentOutOfRangeException()
            };
               
             var uiElement =  _container.InstantiatePrefab(prefab, _uiElementsHandler.transform).GetComponent<GameUIItemView>();
             uiElement.Init(gameItemView.Data);
             
             gameItemView.transform.DOMove(uiElement.transform.position, 0.5f).OnComplete(() =>
             {
                 uiElement.AddElementAnimation(()=>OnUIElementAdded(uiElement));
                 gameItemView.DestroyObject();
             });
             
        }
        
        private void ChecGameState()
        {
            if (_uiItems.Count >= GAME_OVER_UI_ELEMENTS_COUNT)
            {
                OnGameOver?.Invoke();
            }

            if (_gameItems.Count == 0)
            {
                OnGameCompleted?.Invoke();
            }
        }

        public void OnUIElementAdded(GameUIItemView gameUIItemView)
        {
            _uiItems.Add(gameUIItemView);
            
            ChecGameState();
        }
    }
}