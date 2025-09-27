using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Common
{
    [Serializable]
    public class GameData
    {
        public int currentPoints = 0;
        public List<int> neededPointSpot;
        public List<int> neededPointMap;
        public int clearPoint = 1000;

        public List<GameObject> spotList;
        public List<GameObject> mapList; 
        public List<GameObject> clearList;
    }
}
