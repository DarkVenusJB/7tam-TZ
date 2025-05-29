using System.Collections.Generic;
using Scripts.View;

namespace Scripts.Services
{
    public interface IGameStateService
    {
        EGameState GameState { get; }
        List<GameItemView> GameItems { get; }
        void GameStarted();
        void AddNewElement();
    }
}