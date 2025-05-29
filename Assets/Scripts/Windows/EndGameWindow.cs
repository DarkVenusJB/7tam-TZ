using UnityEngine;

namespace Scripts.Windows
{
    public class EndGameWindow : MonoBehaviour
    {
        public void Show(bool isLevelComplited)
        {
            gameObject.SetActive(true);
        }
    }
}