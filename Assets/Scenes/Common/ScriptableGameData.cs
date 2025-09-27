namespace Scenes.Common
{
    using System.Collections.Generic;
    using UnityEngine;

    namespace Scenes.Common
    {
        [CreateAssetMenu(fileName = "ScriptableGameData", menuName = "Game/ScriptableGame Data", order = 0)]
        public class ScriptableGameData : ScriptableObject
        {
            [Header("Point Settings")]
            public int currentPoints = 0;
            public int clearPoint = 1000;
        }
    }
}