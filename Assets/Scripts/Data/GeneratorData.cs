using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(fileName = "GeneratorData", menuName = "Data/GeneratorData")]
    public class GeneratorData : ScriptableObject
    {
        [SerializeField] private Vector2Int _minMaxItemsCount;
        [SerializeField] private Vector2Int _minMaxElementsWithMechanics;
        
        public Vector2Int MinMaxItemsCount => _minMaxItemsCount;
        public Vector2Int MinMaxElementsWithMechanics => _minMaxElementsWithMechanics;
    }
}