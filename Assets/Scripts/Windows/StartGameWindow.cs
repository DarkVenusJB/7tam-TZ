using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Windows
{
    public class StartGameWindow  : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        
        public void Init( Action onGameStarted)
        {
            if(! gameObject.activeSelf)
                gameObject.SetActive(true);
            
            _startGameButton.onClick.AddListener((() =>
            {
                gameObject.SetActive(false);
                
                onGameStarted?.Invoke();
            }));
        }
    }
}