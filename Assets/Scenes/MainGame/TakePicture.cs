using System;
using UnityEngine;

namespace Scenes.MainGame
{
    public class TakePicture : MonoBehaviour
    {
        GameData _gameData;
        GameManager _gameManager;
        public GameObject photo;
        

        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _gameData =  FindObjectOfType<GameData>();
        }

        bool condition = true;
        
        public void CheckPoint()
        {
            if (condition)
            {
                _gameData.currentPoints += 1;
                Out();
            }
        }

        public void Out()
        {
            _gameManager.currentSpot.SetActive(false);
            _gameManager.scoreObject.SetActive(true);
            photo.SetActive(false);
            _gameManager.ResetPoints();
        }
    }
}
