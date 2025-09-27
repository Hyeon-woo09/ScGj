using System;
using UnityEngine;
using UnityEngine.UI;


namespace Scenes.MainGame
{
    public class ButtonOnclick : MonoBehaviour
    {
        GameManager _gameManager;
        Button _button;
        
        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _button =  GetComponent<Button>();
        }

        void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        public void OnButtonClicked()
        {
            _gameManager.currentSpot = this.gameObject; 
        }
    }
}
