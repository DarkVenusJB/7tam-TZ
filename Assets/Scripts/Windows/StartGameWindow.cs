using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Windows
{
    public class StartGameWindow  : MonoBehaviour, IStartGameWindow
    {
        [SerializeField] private Button _startGameButton;
        
        public void Init()
        {
            if(! gameObject.activeSelf)
                gameObject.SetActive(true);
            
            _startGameButton.onClick.AddListener((() =>
            {
                
            }));
        }
    }
}