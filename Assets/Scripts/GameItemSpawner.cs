using Scripts.Data;
using Scripts.Services;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public class GameItemSpawner : MonoBehaviour, IGameItemSpawner
    {
        [Inject] private IGameItemFactory _factory;

        public void Spawn(GameItemData data, Vector3 position)
        {
            var view = _factory.Create(data);
            view.transform.position = position;
        }
    }
}