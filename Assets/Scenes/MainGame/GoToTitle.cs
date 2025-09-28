using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes.MainGame
{
    public class GoToTitle : MonoBehaviour
    {
        Button _button;
        
        void Awake()
        {
            _button =  GetComponent<Button>();
        }

        void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        public void OnButtonClicked()
        {
            SceneManager.LoadScene("Title");
        }
    }
}
