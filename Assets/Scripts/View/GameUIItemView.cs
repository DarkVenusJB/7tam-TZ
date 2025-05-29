using System;
using DG.Tweening;
using Providers;
using Scripts.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.View
{
    public class GameUIItemView : MonoBehaviour, IGameItemView
    {
        [SerializeField] private Image _animalImage;
        [SerializeField] private Image _figureImage;
        [SerializeField] private GameObject _scaledObject;
        
        private Vector3 _baseScale;
        
        [Inject] private IGameItemVisualProvider  _visualProvider;

        public void Init(GameItemData data)
        {
            _baseScale = _scaledObject.transform.localScale;
            
           _figureImage.color = _visualProvider.GetColor(data.ColorType);
           _animalImage.sprite = _visualProvider.GetAnimalSprite(data.AnimalType);
           
           _scaledObject.transform.localScale = Vector3.zero;
           
        }

        public void AddElementAnimation(Action onElementAddFinished)
        {
            Sequence seq = DOTween.Sequence();
            
            seq.Append(transform.DOScale(_baseScale*1.1f, 0.5f))
                .Append(transform.DOScale(_baseScale, 0.1f)).OnComplete(() =>
                {
                    onElementAddFinished?.Invoke();
                });
            
            seq.Play();
            
        }
    }
}