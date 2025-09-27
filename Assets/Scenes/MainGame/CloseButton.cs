using UnityEngine;
using UnityEngine.UI;

namespace Scenes.MainGame
{
    public class CloseButton : MonoBehaviour
    {
        TakePicture _takePicture;
        Button _button;
        
        void Awake()
        {
            _takePicture = FindObjectOfType<TakePicture>();
            _button =  GetComponent<Button>();
        }

        void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        public void OnButtonClicked()
        {
           _takePicture.Out();
        }
    }
}