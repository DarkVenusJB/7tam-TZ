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
		
		private IItemsGeneratorService _itemsGeneratorService;
		private IGameStateService _gameStateService;
		
		private const float LOADING_WINDOW_DURATION = 0.5f;

		[Inject]
		public void Inject(
			IItemsGeneratorService  itemsGeneratorService, 
			IGameStateService gameStateService)
		{
			_itemsGeneratorService = itemsGeneratorService;
			_gameStateService = gameStateService;
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

			await UniTask.WaitUntil(() => _itemsGeneratorService.IsAllItemsReady);
			
			await UniTask.Delay(1500);
			
			_loadingWindow.DOFade(0, LOADING_WINDOW_DURATION)
				.OnComplete(() =>
				{
					_loadingWindow.gameObject.SetActive(false);
				});
			
			_startGameWindow.Init();
		}
	}
}

