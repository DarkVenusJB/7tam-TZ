using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts.Data;
using Scripts.Services;
using Scripts.Windows;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Scripts
{
	public class GameSceneSetup : MonoBehaviour
	{
		[Header("GeneratorData")]
		[SerializeField] private GeneratorData _generatorData;
		[Header("Game Scene Setup")]
		[SerializeField] private BoxCollider2D _spawnZoneCollider;
		[SerializeField] private StartGameWindow _startGameWindow;
		[SerializeField] private EndGameWindow _endGameWindow;
		[SerializeField] private CanvasGroup _loadingWindow;
		
		private List<GameItemData> _generatedElements = new();
		
		private IItemsGeneratorService _itemsGeneratorService;
		private IGameStateService _gameStateService;
		private IGameItemSpawner _itemSpawner;

		private const float LOADING_WINDOW_DURATION = 0.5f;
		private const float ELEMENT__SPAWN_DELAY = 0.2f;

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

		private void OnEnable()
		{
			_gameStateService.OnGameCompleted += OnGameCompleted;
			_gameStateService.OnGameOver += OnGameOver;
		}

		private void OnDisable()
		{
			_gameStateService.OnGameCompleted -= OnGameCompleted;
			_gameStateService.OnGameOver -= OnGameOver;
		}

		private async void DifferedStart()
		{
			_loadingWindow.alpha = 1;
			
			await UniTask.WaitWhile(() => _itemsGeneratorService == null);
			await UniTask.WaitWhile(() => _gameStateService == null);
			await UniTask.WaitWhile(() => _itemSpawner == null);

			_generatedElements =_itemsGeneratorService.GenerateItems(_generatorData);
			
			await UniTask.WaitUntil(() => _itemsGeneratorService.IsAllItemsReady);
			
			await UniTask.Delay(500);
			
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
				var item =  _itemSpawner.Spawn(element, GetRandomPointInside());
				
				_gameStateService.GameItems.Add(item);
				
				await UniTask.Delay(TimeSpan.FromSeconds(ELEMENT__SPAWN_DELAY));
			}
		}

		private void OnGameCompleted()
		{
			_endGameWindow.Show(true);
		}

		private void OnGameOver()
		{
			_endGameWindow.Show(false);
		}
		
		private Vector3 GetRandomPointInside()
		{
			var bounds = _spawnZoneCollider.bounds;

			float x = Random.Range(bounds.min.x, bounds.max.x);
			float y = Random.Range(bounds.min.y, bounds.max.y);

			return new Vector3(x, y, 0f);
		}
	}
}

