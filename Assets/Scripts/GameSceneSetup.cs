using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts.Data;
using Scripts.Services;
using Scripts.Windows;
using UnityEngine;
using Zenject;

namespace Scripts
{
	public class GameSceneSetup : MonoBehaviour
	{
		[Header("GeneratorData")]
		[SerializeField] private GeneratorData _generatorData;
		[Header("Game Scene Setup")]
		[SerializeField] private StartGameWindow _startGameWindow;
		[SerializeField] private CanvasGroup _loadingWindow;
		
		private List<GameItemData> _generatedElements = new();
		
		private IItemsGeneratorService _itemsGeneratorService;
		private IGameStateService _gameStateService;
		private IGameItemSpawner _itemSpawner;

		private const float LOADING_WINDOW_DURATION = 0.5f;

		[Inject]
		public void Inject(
			IItemsGeneratorService  itemsGeneratorService, 
			IGameStateService gameStateService,
			IGameItemSpawner  itemSpawner)
		{
			_itemsGeneratorService = itemsGeneratorService;
			_gameStateService = gameStateService;
			_itemSpawner = itemSpawner;
		}
		
		private void Start()
		{
			DifferedStart();
		}

		private async void DifferedStart()
		{
			_loadingWindow.alpha = 1;
			
			await UniTask.WaitWhile(() => _itemsGeneratorService == null);
			await UniTask.WaitWhile(() => _gameStateService == null);
			await UniTask.WaitWhile(() => _itemSpawner == null);

			_generatedElements =_itemsGeneratorService.GenerateItems(_generatorData);
			
			await UniTask.WaitUntil(() => _itemsGeneratorService.IsAllItemsReady);
			
			await UniTask.Delay(1500);
			
			_loadingWindow.DOFade(0, LOADING_WINDOW_DURATION)
				.OnComplete(() =>
				{
					_loadingWindow.gameObject.SetActive(false);
				});
			
			_startGameWindow.Init(() => StartSpawnElementsWithDelay().Forget());
		}

		private async UniTaskVoid StartSpawnElementsWithDelay()
		{
			foreach (var element in _generatedElements)
			{
				_itemSpawner.Spawn(element, Vector3.zero);
				
				await UniTask.Delay(400);
			}
		}
	}
}

