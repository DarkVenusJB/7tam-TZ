using DG.Tweening;
using Providers;
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
            InitProviders();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<ItemsGeneratorService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle().NonLazy();
        }

        private void InitProviders()
        {
            Container.BindInterfacesAndSelfTo<GameItemVisualProvider>().AsSingle().NonLazy();
        }
    }
}
