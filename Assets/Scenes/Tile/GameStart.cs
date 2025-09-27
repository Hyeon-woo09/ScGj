using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes.Tile
{
    public class GameStart : MonoBehaviour
    {
        Button _button;

        void Awake()
        {
            _button = GetComponent<Button>();
        }

        void Start()
        {
            _button.onClick.AddListener(LoadScene);
        }

        public void LoadScene()
        {
            SceneManager.LoadScene("MainGame"); 
        }
        
    }
}
