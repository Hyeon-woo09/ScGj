using System;
using Scenes.Common;
using TMPro;
using UnityEngine;

namespace Scenes.MainGame
{
    public class GameManager : MonoBehaviour
    {
        public GameObject currentSpot;
        public GameData gameData;
        public TextMeshProUGUI scoreText;
        public GameObject scoreObject;

        void Awake()
        {
            gameData = FindAnyObjectByType<GameDataManager>().gameData;
        }

        public void ResetPoints()
        {
            scoreText.text = gameData.currentPoints.ToString();
        }
    }
}
