using System;
using Scenes.Common;
using TMPro;
using UnityEngine;

namespace Scenes.MainGame
{
    public class GameManager : MonoBehaviour
    {
        public GameObject currentSpot;
        public GameDataManager gameDataManager;
        public GameData gameData;
        public TextMeshProUGUI scoreText;
        public GameObject scoreObject;

        void Awake()
        {
            gameDataManager = FindObjectOfType<GameDataManager>();
            gameData = FindAnyObjectByType<GameDataManager>().gameData;
        }

        public void ResetPoints()
        {
            gameDataManager.Load();
            scoreText.text = gameData.currentPoints.ToString();
        }
    }
}
