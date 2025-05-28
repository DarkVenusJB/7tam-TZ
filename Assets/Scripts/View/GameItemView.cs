using System;
using Providers;
using Scripts.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Scripts.View
{
    public class GameItemView : MonoBehaviour, IPointerClickHandler,IGameItemView
    {
        [SerializeField] private SpriteRenderer _figureImage;
        [SerializeField] private SpriteRenderer _animalImage;
        
        [Inject] private IGameItemVisualProvider  _visualProvider;
        
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Init(GameItemData data)
        {
            
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}