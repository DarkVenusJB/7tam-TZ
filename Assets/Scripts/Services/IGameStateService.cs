using System;
using System.Collections.Generic;
using Scripts.View;

namespace Scripts.Services
{
    public interface IGameStateService
    {
        EGameState GameState { get; }
        List<GameItemView> GameItems { get; }
        
        event Action OnGameCompleted;
        public event Action OnGameOver;
        void GameStarted(List<GameItemView> gameItems);
        void AddNewElement(GameItemView gameItemView);
    }
}