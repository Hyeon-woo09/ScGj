using Scenes.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.MainGame
{
    public class Cheat : MonoBehaviour
    {
        GameManager _gameManager;
        Button _button;
        GameData _gameData;
        GameDataManager _gameDataManager;
        Bar _bar;
        
        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _button =  GetComponent<Button>();
            _gameDataManager = FindObjectOfType<GameDataManager>();
            _gameData = _gameDataManager.gameData;
            _bar = FindAnyObjectByType<Bar>();
        }
        
        
        void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        public void OnButtonClicked()
        {
            _gameData.currentPoints += 1;
            _gameDataManager.Save();
            _gameManager.currentSpot = this.gameObject; 
            _gameManager.ResetPoints();
            _gameManager.ShowSpotSetting();
            _bar.UpdateBar();
        }
    }
}
