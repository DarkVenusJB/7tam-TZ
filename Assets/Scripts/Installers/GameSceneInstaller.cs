using DG.Tweening;
using Providers;
using Scripts;
using Scripts.Data;
using Scripts.Services;
using Scripts.View;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameItemView _circlePrefab;
        [SerializeField] private GameItemView _squarePrefab;
        [SerializeField] private GameItemView _trianglePrefab;
        
        [SerializeField] private GameItemViewData _visualData;
        
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
            
            Container.Bind<IGameItemFactory>().To<GameItemFactory>().AsSingle()
                .WithArguments(_circlePrefab, _squarePrefab, _trianglePrefab);
            
            Container.Bind<IGameItemSpawner>().To<GameItemSpawner>()
                .FromComponentsInHierarchy().AsSingle();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<ItemsGeneratorService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle().NonLazy();
        }

        private void InitProviders()
        {
            Container.BindInterfacesAndSelfTo<GameItemVisualProvider>().AsSingle().WithArguments(_visualData);
        }
    }
}
