using System;
using DG.Tweening;
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
        private Collider2D _collider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        public void Init(GameItemData data)
        {
            _animalImage.sprite = _visualProvider.GetAnimalSprite(data.AnimalType);
            _figureImage.color = _visualProvider.GetColor(data.ColorType);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Obj selected");
            
            _collider.enabled = false;
            _rigidbody.gravityScale = 0;
            
            transform.SetAsLastSibling();
            transform.DOMove(new Vector3(0,50f,0), 0.5f);
        }
    }
}