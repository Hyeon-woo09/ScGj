using System;
using Scenes.Common.Scenes.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes.Tile
{
    public class GameStart : MonoBehaviour
    {
        Button _button;
        public ScriptableGameData scriptableGameData;

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
            print(scriptableGameData.currentPoints);
            
            if (scriptableGameData != null)
            {
                scriptableGameData.currentPoints = 0;
            }
            
            SceneManager.LoadScene("MainGame"); 
        }
    }
}
