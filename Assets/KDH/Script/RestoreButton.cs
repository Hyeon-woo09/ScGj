using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RestoreButton : MonoBehaviour
{
    public enum MiniGames
    {
        ReedPlant,
        EggProtect,
        WaterwayRestore
    }

    public MiniGames playMiniGame;
    public GameObject[] MiniGamePrefabs;

    public void OnClick()
    {
        Instantiate(MiniGamePrefabs[(int)playMiniGame], transform.parent);
    }
}
