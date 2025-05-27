using System;
using Scripts.Services;
using Scripts.Windows;
using UnityEngine;
using Zenject;

namespace Scripts
{
	public class GameSceneSetup : MonoBehaviour
	{
		[SerializeField] private StartGameWindow _startGameWindow;
		
		private IItemsGeneratorService _itemsGeneratorService;
		private IGameStateService _gameStateService;

		[Inject]
		public void Inject(
			IItemsGeneratorService  itemsGeneratorService, 
			IGameStateService gameStateService)
		{
			_itemsGeneratorService = itemsGeneratorService;
			_gameStateService = gameStateService;
		}
		
		private void Awake()
		{
			throw new NotImplementedException();
		}
	}
}

