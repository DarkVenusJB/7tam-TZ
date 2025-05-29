using Scripts.Data;
using Scripts.Services;
using Scripts.View;
using UnityEngine;
using Zenject;

namespace Scripts
{
    public interface IGameItemSpawner
    {
        GameItemView Spawn(GameItemData data, Vector3 position);
    }
}