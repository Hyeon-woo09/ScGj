using Scenes.Common;
using Scenes.Common.Scenes.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.MainGame
{
    public class Bar : MonoBehaviour
    {
        public ScriptableGameData scriptableGameData; // Inspector에서 드래그하거나 Resources에서 로드 가능
        Image _image;
        GameData _gameData;

        void Awake()
        {
            _gameData = FindAnyObjectByType<GameDataManager>().gameData;
            _image = GetComponent<Image>();
            
            if (scriptableGameData == null)
            {
                scriptableGameData = Resources.Load<ScriptableGameData>("ScriptableGameData");
            }

            if (scriptableGameData == null)
            {
                Debug.LogError("GameData.asset을 Resources 폴더에 넣었는지 확인하세요!");
            }
        }

        void Start()
        {
            _image.fillAmount = 0;
            UpdateBar();
        }
        
        public void UpdateBar()
        {
            if (scriptableGameData == null) return;
            _image.fillAmount = Mathf.Clamp01(scriptableGameData.currentPoints / 10f);
        }
    }
}