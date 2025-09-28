using System;
using Scenes.Common;
using UnityEngine;

namespace Scenes.MainGame
{
    public class TakePicture : MonoBehaviour
    {
        GameData _gameData;
        GameManager _gameManager;
        GameDataManager _gameDataManager;
        public GameObject photo;
        public GameData gameData;

        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _gameDataManager = FindObjectOfType<GameDataManager>();
            _gameData = _gameDataManager.gameData;
        }
        
        public void CheckPoint()
        {
            _gameData.currentPoints += 1;
            _gameDataManager.Save();
            Out();
        }

        public void Out()
        {
            _gameManager.ResetPoints();
            _gameManager.currentSpot.SetActive(false);
            _gameManager.scoreObject.SetActive(true);
            _gameManager.ShowSpotSetting();
        }
    }
}
