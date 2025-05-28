using Scripts.Data;
using Scripts.Services;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public interface IGameItemSpawner
    {
        void Spawn(GameItemData data, Vector3 position);
    }
}