using Providers;
using Scripts.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.View
{
    public class GameUIItemView : MonoBehaviour, IGameItemView
    {
        [SerializeField] private Image _figureImage;
        [SerializeField] private Image _animalImage;
        [SerializeField] private Color _figureColor;
        
        [Inject] private IGameItemVisualProvider  _visualProvider;
        
        public void Init(GameItemData data)
        {
            throw new System.NotImplementedException();
        }
    }
}