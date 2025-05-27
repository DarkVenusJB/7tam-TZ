using DG.Tweening;
using Scripts.Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DOTween.Init();
        }

        public override void InstallBindings()
        {
            InstallServices();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<ItemsGeneratorService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<IGameStateService>().AsSingle().NonLazy();
        }
    }
}
