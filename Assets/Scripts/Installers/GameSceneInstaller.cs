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
        [Header("Game Elements")]
        [SerializeField] private GameItemView _circlePrefab;
        [SerializeField] private GameItemView _squarePrefab;
        [SerializeField] private GameItemView _trianglePrefab;
        [SerializeField] private GameItemView _capsulePrefab;
        [Header("UI Elements")]
        [SerializeField] private RectTransform _uiElementsContainer; 
        [SerializeField] private GameUIItemView _circleUIPrefab;
        [SerializeField] private GameUIItemView _squareUIPrefab;
        [SerializeField] private GameUIItemView _triangleUIPrefab;
        [SerializeField] private GameUIItemView _capsuleUIPrefab;
        [Header("Data")]
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
                .WithArguments(_circlePrefab, _squarePrefab, _trianglePrefab, _capsulePrefab);
            
            Container.Bind<IGameItemSpawner>().To<GameItemSpawner>()
                .FromComponentsInHierarchy().AsSingle();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<ItemsGeneratorService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle()
                .WithArguments(_uiElementsContainer,_circleUIPrefab, _squareUIPrefab, _triangleUIPrefab, _capsuleUIPrefab);
        }

        private void InitProviders()
        {
            Container.BindInterfacesAndSelfTo<GameItemVisualProvider>().AsSingle().WithArguments(_visualData);
        }
    }
}
