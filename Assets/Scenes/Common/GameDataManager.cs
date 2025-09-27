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
        
        ScriptableGameData scriptableGameData;

        void Start()
        {
            scriptableGameData = Resources.Load<ScriptableGameData>("ScriptableGameData");
            SetNeededPointToSpot();
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
            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(SavePath, json);
            Debug.Log("게임 저장 완료: " + SavePath);

            scriptableGameData.currentPoints = gameData.currentPoints;

        }

        // JSON에서 불러오기
        public void Load()
        {
            if (!File.Exists(SavePath))
            {
                Debug.LogWarning("세이브 파일 없음");
                return;
            }

            string json = File.ReadAllText(SavePath);
            gameData = JsonUtility.FromJson<GameData>(json);

            Debug.Log("게임 불러오기 완료");

            gameData.currentPoints = scriptableGameData.currentPoints;
        }
    }
}