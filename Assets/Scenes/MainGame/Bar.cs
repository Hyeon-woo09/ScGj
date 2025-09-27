using Scenes.Common;
using Scenes.Common.Scenes.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.MainGame
{
    public class Bar : MonoBehaviour
    {
        public ScriptableGameData scriptableGameData; 
        Image _image;
        float BarArcSize => 15f;

        void Awake()
        {
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
            _image.fillAmount = Mathf.Clamp01(scriptableGameData.currentPoints / BarArcSize);
        }
    }
}