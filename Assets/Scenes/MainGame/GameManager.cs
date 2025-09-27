using System;
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

        public void ResetPoints()
        {
            scoreText.text = gameData.currentPoints.ToString();
        }
    }
}
