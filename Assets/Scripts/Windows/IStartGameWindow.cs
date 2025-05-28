using System;

namespace Scripts.Windows
{
    public interface IStartGameWindow
    {
        void Init(Action onGameStarted);
    }
}