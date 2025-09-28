using System;
using System.Collections.Generic;
using Scenes.Common.Scenes.Common;
using Scenes.MainGame;

namespace Scenes.Common
{
    using UnityEngine;
    using System.IO;
    using System.Linq;

    public class GameDataManager : MonoBehaviour
    {
        public GameData gameData = new GameData();
        string SavePath => Path.Combine(Application.persistentDataPath, "gamedata.json");
        
        ScriptableGameData _scriptableGameData;

        void Start()
        {
            _scriptableGameData = Resources.Load<ScriptableGameData>("ScriptableGameData");
            SetNeededPointToSpot();
            SetNeededPointToMap();
        }

        [ContextMenu("SaveNeededPoint")]
        public void SaveNeededPoint()
        {
            Save();
        }
        
        [ContextMenu("LoadNeededPoint")]
        public void LoadNeededPoint()
        {
            Load();
        }
        
        [ContextMenu("SetNeededPointToSpot")]
        public void SetNeededPointToSpot()
        {
            for (int i = 0; i < gameData.neededPointSpot.Count; i++)
            {
                gameData.spotList[i].GetComponent<ButtonOnclick>().neededPoint = gameData.neededPointSpot[i];
            }
        }
        
        [ContextMenu("SetNeededPointToMap")]
        public void SetNeededPointToMap()
        {
            for (int i = 0; i < gameData.neededPointMap.Count; i++)
            {
                gameData.mapList[i].GetComponent<ButtonOnclick>().neededPoint = gameData.neededPointMap[i];
            }
        }
        
        // JSON으로 저장
        public void Save()
        {
            print("게임 저장 완료");
            _scriptableGameData.currentPoints = gameData.currentPoints;

        }

        // JSON에서 불러오기
        public void Load()
        {
            print("게임 불러오기 완료");

            gameData.currentPoints = _scriptableGameData.currentPoints;
        }
    }
}